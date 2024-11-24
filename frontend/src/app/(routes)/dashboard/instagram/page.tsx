"use client";

import SchedulePost from "@/app/(routes)/dashboard/instagram/_components/schedule-post";
import TabsView from "@/components/tabs-view";
import { useSession } from "next-auth/react";
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

export default function InstagramPage() {

    const tabs: TabContent[] = [
        {
            value: "schedule-post",
            title: "Planuj post",
            content: <SchedulePost />,
        },
        {
            value: "schedule-reel",
            title: "Planuj rolkę",
            content: <h1>Planuj rolkę</h1>,
        }
    ];

    const { data: session } = useSession();
    const [igUserDetail, setIgUserDetail] = useState<InstagramUserDetail | null>(null);
    const [isUserLoading, setIsUserLoading] = useState(true);

    useEffect(() => {
        if (session) {
            const fetchInstagramUser = async () => {
                const userDetail = await getInstagramUser(session.user.token.token as string);
                setIgUserDetail(userDetail);
                setIsUserLoading(false);
            };
            fetchInstagramUser();
        }
    }, [session]);

    return (
        <main className="w-full h-full flex justify-center items-start space-x-12">
            <div className="w-fit flex flex-col space-y-4">
                <InstagramProfile user={igUserDetail} isLoading={isUserLoading} />

                <Dialog>
                    <DialogTrigger asChild>
                        <Button variant="outline">
                            <Plus />
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
                        <TabsView tabs={tabs} />
                    </DialogContent>
                </Dialog>

            </div>

            <div className="w-full max-w-4xl">
                <PostsTable />
            </div>
        </main>
    );
}