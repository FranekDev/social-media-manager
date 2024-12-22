"use client";

import { Button } from "@/components/ui/button";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog";
import { MoreVertical } from "lucide-react";
import React, { useState } from "react";
import { useAuth } from "@/hooks/use-auth";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/data-table";
import { ApiResponse } from "@/types/api/api-response";
import { FacebookPagePostComment } from "@/types/facebook/facebook-page-post-comment";
import { parseDateToLocale } from "@/lib/utils";

type CommentsListProps = {
    title: string;
    description: string | React.JSX.Element;
    postId: string;
    fetchCommentsAction(): Promise<ApiResponse<FacebookPagePostComment[]>>;
};

export default function CommentsList({ title, description, postId, fetchCommentsAction }: CommentsListProps) {

    const { token } = useAuth();
    const [fbPagePostComments, setFbPagePostComments] = useState<FacebookPagePostComment[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [repliesCount, setRepliesCount] = useState(0);
    const [replyCountMap, setReplyCountMap] = useState<{ [commentId: string]: number }>({});
    const [commentsFetched, setCommentsFetched] = useState(false);


    const fetchFacebookPagePostComments = async () => {

        if (commentsFetched) return;

        console.log("fetch");
        const { data, errors } = await fetchCommentsAction();

        // const data = [];
        if (data != null) {
            setFbPagePostComments(data);
            setReplyCountMap(prev => ({
                ...prev,
                commentId: data?.length ?? 0
            }));
            setRepliesCount(data.length);
            setIsLoading(false);
            setCommentsFetched(true);
        }
    };

    const columns: ColumnDef<FacebookPagePostComment>[] = [
        {
            accessorKey: "message",
            header: "Treść"
        },
        {
            accessorKey: "timestamp",
            header: "Data publikacji",
            cell: (info) => parseDateToLocale(info.row.original.createdTime)
        },
    ];


    // if (parent === "media") {
    //     columns.push(
    //         {
    //             header: "Odpowiedzi",
    //             cell: (info) => {
    //
    //                 // const commentId = info.row.original.id;
    //                 // getFacebookPagePostComments(token, "", commentId)
    //                 //     .then(replies => setRepliesCount(replies.data?.length ?? 0));
    //                 // console.log(commentId);
    //                 return <>
    //                     {repliesCount}
    //                     {/*<CommentsList parent="comment"*/}
    //                     {/*              title="Odpowiedzi na komentarz"*/}
    //                     {/*              description={<>*/}
    //                     {/*                  {"Komentarz:"}<br/>*/}
    //                     {/*                  {info.row.original.text}*/}
    //                     {/*              </>}*/}
    //                     {/*              itemId={commentId}*/}
    //                     {/*              fetchCommentsAction={async () => await getInstagramCommentReplies(token, commentId)}/>*/}
    //                 </>;
    //             },
    //         },
    //         {
    //             header: "Odpowiedz",
    //             cell: (info) => {
    //                 return <AnswerCommentDialog comment={info.row.original.message}
    //                                             commentId={info.row.original.id}
    //                                             updateCommentsCountAction={fetchFacebookPagePostComments}/>;
    //             },
    //         }
    //     );
    // }

    return (
        <Dialog onOpenChange={open => {
            if (open) {
                fetchFacebookPagePostComments();
            }
        }}>
            <DialogTrigger asChild>
                <Button variant="ghost"
                        asChild
                        className="p-0 m-0 w-fit h-fit cursor-pointer">
                    <MoreVertical size={16}/>
                </Button>
            </DialogTrigger>
            <DialogContent className="">
                <DialogHeader>
                    <DialogTitle>{title}</DialogTitle>
                    <DialogDescription>{description}</DialogDescription>
                </DialogHeader>
                <div className="grid gap-4 py-4">
                    <DataTable columns={columns}
                               data={fbPagePostComments}
                               isLoading={isLoading}/>
                </div>
            </DialogContent>
        </Dialog>
    );
}