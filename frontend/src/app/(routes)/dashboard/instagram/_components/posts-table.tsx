"use client";

import { useEffect, useState } from "react";
import { getInstagramUserMedia } from "@/features/instagram/api/get-instagram-user-media";
import { InstagramMediaDetail } from "@/types/instagram/response/instagram-media-detail";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/data-table";
import Image from "next/image";
import { getMediaTypeString, InstagramMediaType } from "@/types/instagram/enums/instagram-media-enums";
import { useAuth } from "@/hooks/use-auth";
import CommentsList from "@/app/(routes)/dashboard/instagram/_components/comments-list";
import { getInstagramPostComments } from "@/features/instagram/api/get-instagram-post-comments";
import InstagramMediaInsights from "@/app/(routes)/dashboard/instagram/_components/instagram-media-insights";
import { InstagramMediaInsightType } from "@/types/instagram/enums/instagram-media-insight-type";
import { useToast } from "@/hooks/use-toast";

export default function PostsTable() {
    const { token } = useAuth();
    const [igUserMedia, setIgUserMedia] = useState<InstagramMediaDetail[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const {toast} = useToast();

    useEffect(() => {
        const fetchInsagramUserMedia = async () => {
            const { data, errors } = await getInstagramUserMedia(token);

            if (errors.length > 0) {
                errors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.message
                    });
                });

            }
            else {
                setIgUserMedia((data ?? []) as InstagramMediaDetail[]);
            }
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
            cell: (info) => (<>
                <div className="flex justify-start items-center">
                    <p>{info.row.original.commentsCount}</p>
                    <CommentsList itemId={info.row.original.id}
                                  parent={"media"}
                                  title="Komentarze użytkowników"
                                  description="Tutaj możesz zobaczyć komentarze, które użytkownicy zostawili pod Twoim postem."
                                  fetchCommentsAction={async () =>  await getInstagramPostComments(token, info.row.original.id)}/>
                </div>
            </>),
        },
        {
            header: "Szczegóły",
            cell: (info) => (
                // <Button
                    // onClick={() => openDialog(info.row.original.id)}
                    // variant="ghost"
                    // className="rounded-full w-fit h-fit p-1 m-1"
                // >
                //     <Info size={48} color="grey" />
                // </Button>
                <InstagramMediaInsights mediaId={info.row.original.id}
                                        insightType={info.row.original.mediaType === InstagramMediaType.Video
                                            ? InstagramMediaInsightType.REEL
                                            : InstagramMediaInsightType.POST} />
            ),

        }
    ];

    return <DataTable columns={columns}
                      data={igUserMedia}
                      isLoading={isLoading}/>;
}