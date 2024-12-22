"use client";

import { useEffect, useState } from "react";
import { FacebookPublishedPost } from "@/types/facebook/facebook-published-post";
import { useAuth } from "@/hooks/use-auth";
import { getFacebookPublishedPosts } from "@/features/facebook/api/get-facebook-published-posts";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/data-table";
import { useToast } from "@/hooks/use-toast";
import { parseDateToLocale } from "@/lib/utils";
import { getFacebookPagePostComments } from "@/features/facebook/api/get-facebook-page-post-comments";
import CommentsList from "@/app/(routes)/dashboard/facebook/_components/comments-list";

type FacebookPublishedPostsProps = {
    pageId: string;
};

export default function FacebookPublishedPosts({ pageId }: FacebookPublishedPostsProps) {

    const { token } = useAuth();
    const { toast } = useToast();
    const [fbPublishedPosts, setFbPublishedPosts] = useState<FacebookPublishedPost[]>();
    const [isLoading, setIsLoading] = useState(true);
    const [commentsCounts, setCommentsCount] = useState<{ [postId: string]: number }>({});

    useEffect(() => {
        const fetchFbPublishedPosts = async () => {
            const { data, errors } = await getFacebookPublishedPosts(token, pageId);

            if (errors.length > 0) {
                errors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.message
                    });
                });
            }

            setFbPublishedPosts(data ?? []);
            setIsLoading(false);
        };

        fetchFbPublishedPosts();

    }, [token, pageId]);

    useEffect(() => {
        if (fbPublishedPosts) {
            fbPublishedPosts.forEach(post => {
                getFacebookPagePostComments(token, post.id, post.id)
                    .then(comments => {
                        setCommentsCount(prev => ({
                            ...prev,
                            [post.id]: comments.data?.length ?? 0
                        }));
                    });
            })
        }
    }, [fbPublishedPosts, token]);


    const columns: ColumnDef<FacebookPublishedPost>[] = [
        {
            accessorKey: "message",
            header: "Treść"
        },
        {
            accessorKey: "createdTime",
            header: "Data publikacji",
            cell: info => parseDateToLocale(info.row.original.createdTime)
        },
        {
            header: "Komentarze",
            cell: info => {
                const postId = info.row.original.id;

                return (
                    <div className="flex justify-start items-center">
                        <p>{commentsCounts[postId] ?? 0}</p>
                        <CommentsList postId={postId}
                                      title="Komentarze użytkowników"
                                      description="Tutaj możesz zobaczyć komentarze, które użytkownicy zostawili pod Twoim postem."
                                      fetchCommentsAction={async () => await getFacebookPagePostComments(token, info.row.original.id, info.row.original.id)}/>
                    </div>);
            }
        }
    ];

    return <DataTable columns={columns}
                      data={fbPublishedPosts ?? []}
                      isLoading={isLoading}/>;
}