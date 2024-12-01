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
import React, { useState } from "react";
import { InsightDataItem, InstagramInsightsResponse } from "@/types/instagram/response/instagram media insight";
import { useAuth } from "@/hooks/use-auth";
import { InstagramMediaInsightType } from "@/types/instagram/enums/instagram-media-insight-type";
import { getInstagramMediaInsight } from "@/features/instagram/api/get-instagram-media-insight";
import { useToast } from "@/hooks/use-toast";
import { ColumnDef } from "@tanstack/react-table";

type InstagramMediaInsightsProps = {
    mediaId: string;
    insightType: InstagramMediaInsightType;
};

export default function InstagramMediaInsights({ mediaId, insightType }: InstagramMediaInsightsProps) {

    const { token } = useAuth();
    const { toast } = useToast();
    const [instagramMediaInsights, setInstagramMediaInsights] = useState<InstagramInsightsResponse>();
    const [isLoading, setIsLoading] = useState(true);

    const fetchInsights = async () => {
        try {
            const { data, errors } = await getInstagramMediaInsight(token, { mediaId, insightType });

            if (errors.length > 0) {
                errors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.message
                    });
                });
            }
            else {
                setInstagramMediaInsights(data as InstagramInsightsResponse);
            }
            setIsLoading(false);
        } catch (e) {
            toast({
                variant: "destructive",
                description: "Nie udało się pobrać danych postu"
            });
        }
    }

    const columns: ColumnDef<InsightDataItem>[] = [
        {
            header: "Metryka",
            cell: (info) => (
                <>
                    <p className="font-medium">{info.row.original.title}</p>
                    <span className="text-xs text-muted-foreground">{info.row.original.description}</span>
                </>
            ),
        },
        {
            header: "Wartość",
            cell: (info) => <p className="text-right">{info.row.original.values[0].value}</p>
        },
    ];

    return (
        <Dialog onOpenChange={open => {
            if (open) {
                fetchInsights();
            }
        }}>
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
                    <DialogDescription>Tutaj możesz zobaczyć statystki Twojego postu</DialogDescription>
                </DialogHeader>
                <div className="overflow-y-auto max-h-[80dvh]">

                <div className="w-fit h-full">
                    <DataTable columns={columns}
                               data={instagramMediaInsights?.data ?? []}
                               isLoading={isLoading}/>
                </div>
                </div>
            </DialogContent>
        </Dialog>
    );
}

