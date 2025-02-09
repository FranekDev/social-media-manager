"use client";

import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Info } from "lucide-react";
import { DataTable } from "@/components/data-table";
import React from "react";
import { ColumnDef } from "@tanstack/react-table";
import { TikTokVideoInfo } from "@/types/tiktok/response/tiktok-video-info";
import { Skeleton } from "@/components/ui/skeleton";

type TikTokVideoInfoProps = {
    videoInfo: TikTokVideoInfo | null;
    isLoading: boolean;
};

export default function TiktokVideoInfoView({ videoInfo, isLoading }: TikTokVideoInfoProps) {

    const columns: ColumnDef<TikTokVideoInfo>[] = [
        {
            header: "Tytuł",
            accessorKey: "title",
        },
        {
            header: "Polubienia",
            accessorKey: "likeCount",
        },
        {
            header: "Komentarze",
            accessorKey: "commentCount",
        },
        {
            header: "Udostępnienia",
            accessorKey: "shareCount",
        },
        {
            header: "Wyświetlenia",
            accessorKey: "viewCount",
        },
    ];

    if (isLoading) {
        return (
            <Skeleton className="w-4 h-4"/>
        );
    }

    return (
        <Dialog>
            <DialogTrigger asChild>
                <Button variant="ghost"
                        asChild
                        className="p-0 m-0 w-fit h-fit cursor-pointer">
                    <Info size={16}/>
                </Button>
            </DialogTrigger>
            <DialogContent className="min-w-fit max-w-fit max-h-[90dvh]">
                <DialogHeader>
                    <DialogTitle>Statystyki</DialogTitle>
                    <DialogDescription>
                        Tutaj możesz zobaczyć statystki Twojego postu
                    </DialogDescription>
                </DialogHeader>
                <div className="overflow-y-auto max-h-[80dvh]">
                    <div className="w-fit h-full">
                        {videoInfo === null ?
                            <p>
                                Nie udało się pobrać informacji o poście
                            </p>
                            :
                            <DataTable columns={columns}
                                       data={[videoInfo]}
                                       isLoading={false}/>
                        }
                    </div>
                </div>
            </DialogContent>
        </Dialog>
    );
}