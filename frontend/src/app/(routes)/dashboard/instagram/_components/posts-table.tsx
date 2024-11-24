"use client";

import { useSession } from "next-auth/react";
import { useEffect, useState } from "react";
import { getInstagramUserMedia } from "@/features/instagram/api/get-instagram-user-media";
import { InstagramMediaDetail } from "@/types/instagram/response/instagram-media-detail";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/data-table";
import Image from "next/image";
import { getMediaTypeString, InstagramMediaType } from "@/types/instagram/enums/instagram-media-enums";

export default function PostsTable() {
    const { data: session } = useSession();
    const [igUserMedia, setIgUserMedia] = useState<InstagramMediaDetail[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        if (session) {
            const fetchInsagramUserMedia = async () => {
                const userMedia = await getInstagramUserMedia(session.user.token.token as string);
                setIgUserMedia(userMedia);
                setIsLoading(false);
            }
            fetchInsagramUserMedia();
        }
    }, [session]);

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
        }
    ];

    return <DataTable columns={columns} data={igUserMedia} isLoading={isLoading} />;
}