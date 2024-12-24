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
import { TikTokPhoto } from "@/types/tiktok/response/tiktok-photo";
import { TikTokVideo } from "@/types/tiktok/response/tiktok-video";
import { getTiktokScheduledPhotos } from "@/features/tiktok/api/get-tiktok-scheduled-photos";
import { getTiktokScheduledVideos } from "@/features/tiktok/api/get-tiktok-scheduled-videos";
import { Carousel } from "@/components/ui/carousel";
import { parseDateToLocale } from "@/lib/utils";

export default function ScheduledPage() {
    const { token } = useAuth();
    const { toast } = useToast();

    const [scheduledIgPosts, setScheduledIgPosts] = useState<InstagramPost[]>([]);
    const [isScheduledIgPostsLoading, setIsScheduledIgPostsLoading] = useState(true);

    const [scheduledFbPosts, setScheduledFbPosts] = useState<FacebookFeedPost[]>([]);
    const [isScheduledFbPostsLoading, setIsScheduledFbPostsLoading] = useState(true);

    const [scheduledIgReels, setScheduledIgReels] = useState<InstagramReel[]>([]);
    const [isScheduledIgReelsLoading, setIsScheduledIgReelsLoading] = useState(true);

    const [scheduledTikTokPhotos, setScheduledTikTokPhotos] = useState<TikTokPhoto[]>([]);
    const [isScheduledTikTokPhotosLoading, setIsScheduledTikTokPhotosLoading] = useState(true);

    const [scheduledTikTokVideos, setScheduledTikTokVideos] = useState<TikTokVideo[]>([]);
    const [isScheduledTikTokVideosLoading, setIsScheduledTikTokVideosLoading] = useState(true);

    const [weekScheduledPostsCount, setWeekScheduledPostsCount] = useState(0);

    const [monthName, setMonthName] = useState("");

    const getScheduledPostsCountForCurrentMonth = () => {
        const currentMonth = new Date().getMonth();
        const currentYear = new Date().getFullYear();

        const scheduledPosts = [
            ...scheduledIgReels,
            ...scheduledFbPosts,
            ...scheduledTikTokPhotos,
            ...scheduledTikTokVideos
        ];

        setWeekScheduledPostsCount(scheduledPosts.filter(post => {
                const postDate = new Date(post.scheduledAt);
                return postDate.getMonth() === currentMonth && postDate.getFullYear() === currentYear;
            }).length
        );
    }

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
                const { data: tikTokPhotos, errors: tikTokPhotosErrors } = await getTiktokScheduledPhotos(token);
                const { data: tikTokVideos, errors: tikTokVideosErrors } = await getTiktokScheduledVideos(token);

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
                if (tikTokPhotosErrors?.length > 0) {
                    console.log(tikTokPhotosErrors);
                    showToastErrors(tikTokPhotosErrors);
                }
                if (tikTokVideosErrors?.length > 0) {
                    console.log(tikTokVideosErrors);
                    showToastErrors(tikTokVideosErrors);
                }

                setScheduledIgPosts(igPosts ?? []);
                setIsScheduledIgPostsLoading(false);

                setScheduledIgReels(igReels ?? []);
                setIsScheduledIgReelsLoading(false);

                setScheduledFbPosts(fbPosts ?? []);
                setIsScheduledFbPostsLoading(false);

                setScheduledTikTokPhotos(tikTokPhotos ?? []);
                setIsScheduledTikTokPhotosLoading(false);

                setScheduledTikTokVideos(tikTokVideos ?? []);
                setIsScheduledTikTokVideosLoading(false);
            }
        }

        fetchScheduledPosts();
        getScheduledPostsCountForCurrentMonth();

        setMonthName(getMonthName());
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
            cell: (info) => parseDateToLocale(info.row.original.scheduledAt.toString())
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
            cell: (info) => parseDateToLocale(info.row.original.scheduledAt.toString())
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
            cell: (info) => parseDateToLocale(info.row.original.scheduledAt.toString())
        }
    ];

    const tikTokPhotoColumns: ColumnDef<TikTokPhoto>[] = [
        {
            header: "Tytuł",
            accessorKey: "title"
        },
        {
            header: "Opis",
            accessorKey: "description"
        },
        {
            header: "Zdjęcia",
            accessorKey: "photoUrls",
            cell: ({ row }) => (
                <Carousel>
                    {row.original.photoUrls.map((url, index) => (
                        url ? <Image key={index}
                                     src={url}
                                     width={40}
                                     height={40}
                                     className="rounded-md"
                                     alt={"Zdjęcie"} />
                            : null
                    ))}
                </Carousel>
            ),
        },
        {
            header: "Data publikacji",
            accessorKey: "scheduledAt",
            cell: (info) => parseDateToLocale(info.row.original.scheduledAt.toString())
        }
    ];

    const tikTokVideoColumns: ColumnDef<TikTokVideo>[] = [
        {
            header: "Tytuł",
            accessorKey: "title"
        },
        {
            header: "Film",
            accessorKey: "videoUrl",
            cell: ({ row }) => (
                row.original.videoUrl ?
                    <video src={row.original.videoUrl}
                           width={40}
                           height={40}
                           className="rounded-md"
                           controls />
                    : null
            ),
        },
        {
            header: "Data publikacji",
            accessorKey: "scheduledAt",
            cell: (info) => parseDateToLocale(info.row.original.scheduledAt.toString())
        }
    ];

    const getMonthName = () => {
        const date = new Date();
        return `${date.toLocaleString('pl-PL', { month: 'long' })} ${date.getFullYear()}`;
    };

    return (<>
        <h1 className="text-xl">Zaplanowane publikacje</h1>
        <p className="text-neutral-500">Liczba zaplanowanych postów na aktualny miesiąc ({monthName}): {weekScheduledPostsCount}</p>
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
            } as unknown as ContentType<FacebookFeedPost>,
            {
                title: "Zaplanowane zdjęcia na TikToku",
                isLoading: isScheduledTikTokPhotosLoading,
                data: scheduledTikTokPhotos,
                columns: tikTokPhotoColumns
            } as unknown as ContentType<TikTokPhoto>,
            {
                title: "Zaplanowane filmy na TikToku",
                isLoading: isScheduledTikTokVideosLoading,
                data: scheduledTikTokVideos,
                columns: tikTokVideoColumns
            } as unknown as ContentType<TikTokVideo>
        ]}/>
    </>);
}