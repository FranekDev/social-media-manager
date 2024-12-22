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
import React, { useEffect, useState } from "react";
import { useAuth } from "@/hooks/use-auth";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/data-table";
import { getInstagramCommentReplies } from "@/features/instagram/api/get-instagram-comment-replies";
import AnswerCommentDialog from "@/app/(routes)/dashboard/instagram/_components/answer-comment-dialog";
import { ApiResponse } from "@/types/api/api-response";

type CommentsListProps = {
    parent: "media" | "comment";
    title: string;
    description: string | React.JSX.Element;
    itemId: string;
    fetchCommentsAction(): Promise<ApiResponse<InstagramComment[]>>;
};

export default function CommentsList({ parent, title, description, itemId, fetchCommentsAction }: CommentsListProps) {

    const { token } = useAuth();
    const [instagramComments, setInstagramComments] = useState<InstagramComment[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [repliesCount, setRepliesCount] = useState(0);
    const [replyCountMap, setReplyCountMap] = useState<{ [commentId: string]: number }>({});


    const fetchInstagramComments = async () => {
        const { data, errors } = await fetchCommentsAction();

        if (data != null) {
            setInstagramComments(data);
            setReplyCountMap(prev => ({
                ...prev,
                commentId: data?.length ?? 0
            }));
            setRepliesCount(data.length);
            setIsLoading(false);
        }
    };

    const columns: ColumnDef<InstagramComment>[] = [
        {
            accessorKey: "text",
            header: "Treść"
        },
        {
            accessorKey: "timestamp",
            header: "Data publikacji",
            cell: (info) => new Date(info.row.original.timestamp).toLocaleString('pl-PL', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
                timeZoneName: 'short'
            })
        },
    ];


    if (parent === "media") {
        columns.push(
            {
                header: "Odpowiedzi",
                cell: (info) => {
                    const commentId = info.row.original.id;
                    return <>
                        {replyCountMap[commentId] ?? 0}
                        <CommentsList parent="comment"
                                      title="Odpowiedzi na komentarz"
                                      description={<>
                                          {"Komentarz:"}<br/>
                                          {info.row.original.text}
                                      </>}
                                      itemId={commentId}
                                      fetchCommentsAction={async () => await getInstagramCommentReplies(token, commentId)}/>
                    </>;
                },
            },
            {
                header: "Odpowiedz",
                cell: (info) => {
                    return <AnswerCommentDialog comment={info.row.original.text}
                                                commentId={info.row.original.id}
                                                updateCommentsCountAction={fetchInstagramComments}/>;
                },
            }
        );
    }

    return (
        <Dialog onOpenChange={open => {
            if (open) {
                fetchInstagramComments();
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
                               data={instagramComments}
                               isLoading={isLoading}/>
                </div>
            </DialogContent>
        </Dialog>
    );
}