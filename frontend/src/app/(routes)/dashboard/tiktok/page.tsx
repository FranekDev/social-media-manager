"use client";

import TikTokUserProfile from "@/app/(routes)/dashboard/tiktok/_components/tiktok-user-info";
import { useEffect, useState } from "react";
import { TikTokUser } from "@/types/tiktok/response/tiktok-user";
import { useAuth } from "@/hooks/use-auth";
import { getTiktokUserInfo } from "@/features/tiktok/api/get-tiktok-user-info";
import { useToast } from "@/hooks/use-toast";
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
import TabsView from "@/components/tabs-view";
import PublishedPosts from "@/app/(routes)/dashboard/tiktok/_components/published-posts";

export default function TikTokPage() {

    const { token } = useAuth();
    const { toast } = useToast();
    const [tiktokUser, setTiktokUser] = useState<TikTokUser | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const fetchTiktokUser = async () => {
            const { data, errors } = await getTiktokUserInfo(token);
            if (errors.length > 0) {
                errors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.message
                    });
                });
            }
            setTiktokUser(data?.user ?? null);
            setIsLoading(false);
        };

        fetchTiktokUser();
    }, [token]);

    return (
        <main className="w-full h-full flex justify-center items-start space-x-12">
            <div className="w-fit flex flex-col space-y-4">
                <TikTokUserProfile user={tiktokUser}
                                   isLoading={isLoading}/>

                <Dialog>
                    <DialogTrigger asChild>
                        <Button variant="outline">
                            <Plus/>
                            Utwórz post
                        </Button>
                    </DialogTrigger>
                    <DialogContent className="w-fit max-w-4xl">
                    </DialogContent>
                </Dialog>

            </div>

            <div className="w-full max-w-4xl">
                <PublishedPosts />
            </div>

        </main>
    );
}