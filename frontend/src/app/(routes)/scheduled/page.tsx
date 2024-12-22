"use client";

import { useAuth } from "@/hooks/use-auth";
import { useEffect, useState } from "react";
import { InstagramPost } from "@/types/instagram/response/instagram-post";
import { FacebookFeedPost } from "@/types/facebook/facebook-feed-post";
import { getInstagramScheduledPosts } from "@/features/instagram/api/get-instagram-scheduled-posts";
import { getFacebookScheduledPosts } from "@/features/facebook/api/get-facebook-scheduled-posts";
import ScheduledContent, { ContentType } from "@/app/(routes)/scheduled/_components/scheduled-content";
import { ColumnDef } from "@tanstack/react-table";
import Image from "next/image";
import { InstagramReel } from "@/types/instagram/response/instagram-reel";
import { getInstagramScheduledReels } from "@/features/instagram/api/get-instagram-scheduled-reels";
import { useToast } from "@/hooks/use-toast";
import { ValidationErrorResponse } from "@/types/api/error";

export default function ScheduledPage() {
    const { token } = useAuth();
    const { toast } = useToast();

    const [scheduledIgPosts, setScheduledIgPosts] = useState<InstagramPost[]>([]);
    const [isScheduledIgPostsLoading, setIsScheduledIgPostsLoading] = useState(true);

    const [scheduledFbPosts, setScheduledFbPosts] = useState<FacebookFeedPost[]>([]);
    const [isScheduledFbPostsLoading, setIsScheduledFbPostsLoading] = useState(true);

    const [scheduledIgReels, setScheduledIgReels] = useState<InstagramReel[]>([]);
    const [isScheduledIgReelsLoading, setIsScheduledIgReelsLoading] = useState(true);

    const showToastErrors = (errors: ValidationErrorResponse[]) => {
        errors.forEach(error => {
            toast({
                title: error.message,
                variant: "destructive"
            });
        });
    };

    useEffect(() => {
        const fetchScheduledPosts = async () => {
            if (token) {
                const { data: igPosts, errors: igPostErrors } = await getInstagramScheduledPosts(token);
                const { data: igReels, errors: igReelsErrors } = await getInstagramScheduledReels(token);
                const { data: fbPosts, errors: fbErrors } = await getFacebookScheduledPosts(token);

                if (igPostErrors?.length > 0) {
                    console.log(igPostErrors);
                    showToastErrors(igPostErrors);
                }
                if (igReelsErrors?.length > 0) {
                    console.log(igReelsErrors);
                    showToastErrors(igReelsErrors);
                }
                if (fbErrors?.length > 0) {
                    console.log(fbErrors);
                    showToastErrors(fbErrors);
                }

                setScheduledIgPosts(igPosts ?? []);
                setIsScheduledIgPostsLoading(false);

                setScheduledIgReels(igReels ?? []);
                setIsScheduledIgReelsLoading(false);

                setScheduledFbPosts(fbPosts ?? []);
                setIsScheduledFbPostsLoading(false);
            }
        }

        fetchScheduledPosts();
    }, [token]);

    const igPostColumns: ColumnDef<InstagramPost>[] = [
        {
            header: "Treść",
            accessorKey: "caption"
        },
        {
            header: "Post",
            accessorKey: "mediaUrl",
            cell: (info) => {
                const src = info.row.original.imageUrl;
                return <Image
                    src={src}
                    alt="Post"
                    width={30}
                    height={30}
                    className="rounded-md"
                />;
            },
        },
        {
            header: "Data publikacji",
            accessorKey: "scheduledAt",
            cell: (info) => new Date(info.row.original.scheduledAt).toLocaleString('pl-PL', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
                timeZoneName: 'short'
            })
        }
    ];

    const fbColumns: ColumnDef<FacebookFeedPost>[] = [
        {
            header: "Treść",
            accessorKey: "message"
        },
        {
            header: "Data publikacji",
            accessorKey: "scheduledAt",
            cell: (info) => new Date(info.row.original.scheduledAt).toLocaleString('pl-PL', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
                timeZoneName: 'short'
            })
        }
    ];

    const igReelColumns: ColumnDef<InstagramReel>[] = [
        {
            header: "Opis",
            accessorKey: "caption"
        },
        {
            header: "Film",
            accessorKey: "videoUrl",
            cell: (info) => {
                const src = info.row.original.videoUrl;
                return <video
                    src={src ?? ""}
                    width={30}
                    height={30}
                    className="rounded-md"
                    controls
                />;
            },
        },
        {
            header: "Data publikacji",
            accessorKey: "scheduledAt",
            cell: (info) => new Date(info.row.original.scheduledAt).toLocaleString('pl-PL', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
                timeZoneName: 'short'
            })
        }
    ];

    return (<>
        <h1>Zaplanowane publikacje</h1>
        <ScheduledContent contents={[
            {
                title: "Zaplanowane posty na Instagramie",
                isLoading: isScheduledIgPostsLoading,
                data: scheduledIgPosts,
                columns: igPostColumns
            } as unknown as ContentType<InstagramPost>,
            {
                title: "Zaplanowane rolki na Instagramie",
                isLoading: isScheduledIgReelsLoading,
                data: scheduledIgReels,
                columns: igReelColumns
            } as unknown as ContentType<InstagramReel>,
            {
                title: "Zaplanowane wpisy na Facebooku",
                isLoading: isScheduledFbPostsLoading,
                data: scheduledFbPosts,
                columns: fbColumns
            } as unknown as ContentType<FacebookFeedPost>
        ]}/>
    </>);
}