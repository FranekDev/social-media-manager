"use client";

import { TikTokPhoto } from "@/types/tiktok/response/tiktok-photo";
import { useEffect, useState } from "react";
import { useAuth } from "@/hooks/use-auth";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/data-table";
import { getTiktokPublishedPhotos } from "@/features/tiktok/api/get-tiktok-published-photos";
import { useToast } from "@/hooks/use-toast";
import { Carousel } from "@/components/ui/carousel";
import Image from "next/image";
import { TikTokVideo } from "@/types/tiktok/response/tiktok-video";
import { getTiktokPublishedVideos } from "@/features/tiktok/api/get-tiktok-published-videos";
import { TikTokVideoInfo } from "@/types/tiktok/response/tiktok-video-info";
import { getTiktokPostsStats } from "@/features/tiktok/api/get-tiktok-posts-stats";
import TiktokVideoInfoView from "@/app/(routes)/dashboard/tiktok/_components/tiktok-video-info-view";

export default function PublishedPosts() {

    const { token } = useAuth();
    const { toast } = useToast();

    const [tikTokPhotos, setTikTokPhotos] = useState<TikTokPhoto[]>([]);
    const [isPhotosLoading, setIsPhotosLoading] = useState(true);

    const [tikTokVideos, setTikTokVideos] = useState<TikTokVideo[]>([]);
    const [isVideosLoading, setIsVideosLoading] = useState(true);

    const [tiktokVideosStats, setTiktokVideosStats] = useState<TikTokVideoInfo[]>([]);
    const [isVideosStatsLoading, setIsVideosStatsLoading] = useState(true);

    useEffect(() => {
        const fetchTikTokPhotos = async () => {
            const { data: photosData, errors: photosErrors } = await getTiktokPublishedPhotos(token);
            const { data: videosData, errors: videosErrors } = await getTiktokPublishedVideos(token);
            const {data: videosStatsData, errors: videosStatsErrors} = await getTiktokPostsStats(token);


            if (photosErrors.length > 0) {
                photosErrors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.errorMessage
                    });
                });
            }

            if (videosErrors.length > 0) {
                videosErrors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.errorMessage
                    });
                });
            }

            if (videosStatsErrors.length > 0) {
                videosStatsErrors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.errorMessage
                    });
                });
            }

            setTikTokPhotos(photosData ?? []);
            setIsPhotosLoading(false);

            setTikTokVideos(videosData ?? []);
            setIsVideosLoading(false);

            setTiktokVideosStats(videosStatsData ?? []);
            setIsVideosStatsLoading(false);
        }

        fetchTikTokPhotos();
    }, [token]);

    const getPostInfo = (title: string) => {
        return tiktokVideosStats.find(video => video.title === title) || null;
    }

    const photosColumns: ColumnDef<TikTokPhoto>[] = [
        {
            accessorKey: "title",
            header: "Tytuł",
        },
        {
            accessorKey: "description",
            header: "Opis",
        },
        {
            accessorKey: "photoUrls",
            header: "Zdjęcia",
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
            header: "Szczegóły",
            cell: (info) => (
                <TiktokVideoInfoView isLoading={isVideosStatsLoading} videoInfo={getPostInfo(info.row.original.description) ?? null}/>
            ),
        }
    ];

    const videoColumns: ColumnDef<TikTokVideo>[] = [
        {
            accessorKey: "title",
            header: "Tytuł",
        },
        {
            accessorKey: "videoUrl",
            header: "Film",
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
            header: "Szczegóły",
            cell: (info) => (
                <TiktokVideoInfoView isLoading={isVideosStatsLoading} videoInfo={getPostInfo(info.row.original.title) ?? null}/>
            ),
        }
    ];


    return (
        <div className="w-full h-full flex flex-col justify-start items-center gap-8">
            <div className="flex flex-col gap-2 w-full">
                <h2>Opublikowane zdjęcia</h2>
                <DataTable
                    columns={photosColumns}
                    data={tikTokPhotos}
                    isLoading={isPhotosLoading}
                />
            </div>
            <div className="flex flex-col gap-2 w-full">
                <h2>Opublikowane filmy</h2>
                <DataTable
                    columns={videoColumns}
                    data={tikTokVideos}
                    isLoading={isVideosLoading}
                />
            </div>
        </div>
    );

}