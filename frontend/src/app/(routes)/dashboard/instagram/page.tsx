﻿"use client";

import TabsView from "@/components/tabs-view";
import { getInstagramUser } from "@/features/instagram/api/get-instagram-user";
import { InstagramUserDetail } from "@/types/instagram/response/instagram-user-detail";
import { useEffect, useState } from "react";
import InstagramProfile from "@/app/(routes)/dashboard/instagram/_components/instagram-user";
import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog";
import PostsTable from "@/app/(routes)/dashboard/instagram/_components/posts-table";
import ScheduleContent from "@/app/(routes)/dashboard/instagram/_components/schedule-content";
import { useAuth } from "@/hooks/use-auth";
import { useToast } from "@/hooks/use-toast";
import ConnectInstagramAccount from "@/app/(routes)/dashboard/instagram/_components/connect-instagram-account";

export default function InstagramPage() {

    const tabs: TabContent[] = [
        {
            value: "schedule-post",
            title: "Planuj post",
            content: <ScheduleContent type="post"/>
        },
        {
            value: "schedule-reel",
            title: "Planuj rolkę",
            content: <ScheduleContent type="reel"/>
        }
    ];

    const { token } = useAuth();
    const [igUserDetail, setIgUserDetail] = useState<InstagramUserDetail | null>(null);
    const [isUserLoading, setIsUserLoading] = useState(true);
    const { toast } = useToast();

    useEffect(() => {
        if (token) {
            const fetchInstagramUser = async () => {
                const { data, errors } = await getInstagramUser(token);

                if (errors.length > 0) {
                    errors.forEach(error => {
                        toast({
                            variant: "destructive",
                            description: error.errorMessage
                        });
                    });
                }

                setIgUserDetail(data);
                setIsUserLoading(false);

            };
            fetchInstagramUser();
        }
    }, [token]);

    return igUserDetail === null ? <ConnectInstagramAccount /> : (
        <main className="w-full h-full flex justify-center items-start space-x-12">
            <div className="w-fit flex flex-col space-y-4">
                <InstagramProfile user={igUserDetail}
                                  isLoading={isUserLoading}/>

                <Dialog>
                    <DialogTrigger asChild>
                        <Button variant="outline">
                            <Plus/>
                            Utwórz post
                        </Button>
                    </DialogTrigger>
                    <DialogContent className="w-fit max-w-4xl">
                        <DialogHeader>
                            <DialogTitle>Utwórz post</DialogTitle>
                            <DialogDescription>
                                Zaplanuj publikację postu lub rolki na Instagramie.
                            </DialogDescription>
                        </DialogHeader>
                        <TabsView tabs={tabs} isLoading={false}/>
                    </DialogContent>
                </Dialog>

            </div>

            <div className="w-full max-w-4xl">
                <PostsTable/>
            </div>
        </main>
    );
}