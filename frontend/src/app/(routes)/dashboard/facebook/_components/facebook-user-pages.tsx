"use client";

import { FacebookUserPage } from "@/types/facebook/facebook-user-page";
import { useEffect, useState } from "react";
import { useAuth } from "@/hooks/use-auth";
import { getFacebookUserPages } from "@/features/facebook/api/get-facebook-user-pages";
import { useToast } from "@/hooks/use-toast";
import FacebookPage from "@/app/(routes)/dashboard/facebook/_components/facebook-page";
import TabsView from "@/components/tabs-view";
import FacebookPublishedPosts from "@/app/(routes)/dashboard/facebook/_components/facebook-published-posts";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";
import SchedulePost from "@/app/(routes)/dashboard/facebook/_components/schedule-post";

export default function FacebookUserPages() {

    const { token } = useAuth();
    const { toast } = useToast();
    const [fbUserPage, setFbUserPages] = useState<FacebookUserPage[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [tabs, setTabs] = useState<TabContent[]>([]);

    useEffect(() => {
        const fetchFbUserPages = async () => {
            const { data, errors } = await getFacebookUserPages(token);
            console.log(data);

            if (errors.length > 0) {
                errors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.message
                    });
                });
                return;
            }

            setFbUserPages(data ?? []);
            setIsLoading(false);

            setTabs(data?.map(page => ({
                value: page.pageId,
                title: page.name,
                content: (<>
                    <div className="flex gap-12 justify-center">
                        <div className="flex flex-col space-y-4 w-fit">
                            <FacebookPage key={page.pageId}
                                          pageId={page.pageId}
                                          coverUrl={page.pagePicture?.url ?? ""}/>
                            <Dialog>
                                <DialogTrigger asChild>
                                    <Button variant="outline">
                                        <Plus/>
                                        Utwórz wpis
                                    </Button>
                                </DialogTrigger>
                                <DialogContent className="w-fit max-w-4xl">
                                    <DialogHeader>
                                        <DialogTitle>Utwórz wpis</DialogTitle>
                                        <DialogDescription>
                                            Zaplanuj wpis na swoją stronę na Facebooku.
                                        </DialogDescription>
                                    </DialogHeader>
                                    <SchedulePost pageId={page.pageId}/>
                                </DialogContent>
                            </Dialog>
                        </div>
                        <div>
                            <FacebookPublishedPosts pageId={page.pageId}/>
                        </div>
                    </div>
                </>)
            } as TabContent)) ?? []);
        };

        fetchFbUserPages();
    }, [token]);

    return <TabsView tabs={tabs} isLoading={isLoading} />;
}
