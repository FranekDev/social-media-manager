"use client";

import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
} from "@/components/ui/accordion";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/data-table";

export type ContentType<T> = {
    title: string;
    data: React.JSX.Element;
    columns: ColumnDef<T>[];
    isLoading: boolean;
};

type ScheduledContentProps = {
    contents: ContentType<any>[];
};

export default function ScheduledContent({ contents }: ScheduledContentProps) {
    return (
        <Accordion type="single" collapsible className="w-full">
            {contents.map((content, index) => (
                <AccordionItem key={index} value={content.title}>
                    <AccordionTrigger>{content.title}</AccordionTrigger>
                    <AccordionContent>
                        <DataTable columns={content.columns} data={content.data} isLoading={content.isLoading} />
                    </AccordionContent>
                </AccordionItem>
            ))}
        </Accordion>
    );
}