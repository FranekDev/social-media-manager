"use client";

import { useEffect, useState } from "react";
import { getInstagramUserMedia } from "@/features/instagram/api/get-instagram-user-media";
import { InstagramMediaDetail } from "@/types/instagram/response/instagram-media-detail";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/data-table";
import Image from "next/image";
import { getMediaTypeString, InstagramMediaType } from "@/types/instagram/enums/instagram-media-enums";
import { useAuth } from "@/hooks/use-auth";
import { Info } from "lucide-react";
import { Button } from "@/components/ui/button";

export default function PostsTable() {
    const { token } = useAuth();
    const [igUserMedia, setIgUserMedia] = useState<InstagramMediaDetail[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const fetchInsagramUserMedia = async () => {
            const userMedia = await getInstagramUserMedia(token);
            setIgUserMedia(userMedia);
            setIsLoading(false);
        }
        fetchInsagramUserMedia();
    }, [token]);

    const columns: ColumnDef<InstagramMediaDetail>[] = [
        {
            accessorKey: "caption",
            header: "Opis",
        },
        {
            accessorKey: "mediaUrl",
            header: "Podgląd",
            cell: (info) => {
                const mediaType = info.row.original.mediaType;
                const src = info.getValue() as string;

                return mediaType === InstagramMediaType.Video ? (
                    <video
                        src={src}
                        width={75}
                        height={75}
                        className="rounded-md"
                        controls
                    />
                ) : (
                    <Image
                        src={src}
                        alt="Post"
                        width={75}
                        height={75}
                        className="rounded-md"
                    />
                );
            },
        },
        {
            accessorKey: "mediaType",
            header: "Typ postu",
            cell: (info) => getMediaTypeString(info.getValue() as InstagramMediaType),
        },
        {
            accessorKey: "likeCount",
            header: "Polubienia",
        },
        {
            accessorKey: "commentsCount",
            header: "Komentarze",
        },
        {
            header: "Szczegóły",
            cell: (info) => (
                <Button
                    // onClick={() => openDialog(info.row.original.id)}
                    variant="ghost"
                    className="rounded-full w-fit h-fit p-1 m-1"
                >
                    <Info size={48} color="grey" />
                </Button>
            ),

        }
    ];

    return <DataTable columns={columns}
                      data={igUserMedia}
                      isLoading={isLoading}/>;
}