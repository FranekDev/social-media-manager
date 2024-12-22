"use client";

import { useToast } from "@/hooks/use-toast";
import { z } from "zod";
import React from "react";
import { FormComponentProps, RenderField } from "@/types/form";
import { FormControl, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Textarea } from "@/components/ui/textarea";
import { Controller } from "react-hook-form";
import { DatePicker } from "@nextui-org/date-picker";
import { getLocalTimeZone, now, today } from "@internationalized/date";
import FormComponent from "@/components/form/form-component";
import { useFacebookSchedulePostForm } from "@/app/(routes)/dashboard/facebook/_hooks/use-facebook-schedule-post-form";

type SchedulePostProps = {
    pageId: string;
}

export default function SchedulePost({ pageId }: SchedulePostProps) {
    const { schema, form, onSubmit } = useFacebookSchedulePostForm();
    const { toast } = useToast();

    type FormValues = z.infer<typeof schema>;

    const handleSubmit = async (values: any) => {
        console.log({...values, pageId});
        await onSubmit({...values, pageId});
    };

    const renderFields: RenderField<FormValues>[] = [
        {
            fieldName: "message",
            render: ({ field }) => (
                <FormItem>
                    <FormLabel>Treść</FormLabel>
                    <FormControl>
                        <Textarea
                            placeholder="Treść wpisu na Facebook"
                            rows={5}
                            {...field}
                            value={typeof field.value === "string" ? field.value : ""}
                            onChange={e => field.onChange(e.target.value)}
                        />
                    </FormControl>
                    <FormMessage/>
                </FormItem>
            )
        },
        {
            fieldName: "scheduledAt",
            render: () => (
                <FormItem>
                    <Controller
                        control={form.control}
                        name="scheduledAt"
                        render={({ field: { onChange } }) => (
                            <DatePicker
                                className="max-w-2xl w-full"
                                minValue={today(getLocalTimeZone())}
                                defaultValue={now("Europe/Warsaw")}
                                onChange={(date) => {
                                    const dateString = date.toString().split("[")[0];
                                    onChange(dateString);
                                }}
                                label="Data publikacji"
                                labelPlacement="outside"
                                isRequired={true}
                            />
                        )}
                    />
                    <FormMessage/>
                </FormItem>
            )
        },
        // {
        //     fieldName: "pageId",
        //     render: ({ field }) => (
        //         <FormItem>
        //             <FormControl>
        //                 <input
        //                     type="text"
        //                     {...field}
        //                     value={pageId}
        //                     onChange={e => field.onChange(e.target.value)}
        //                 />
        //             </FormControl>
        //             <FormMessage/>
        //         </FormItem>
        //     )
        // }
    ];

    const formComponentProps: FormComponentProps<FormValues> = {
        form,
        schema,
        renderFields,
        onSubmit: handleSubmit,
        submitLabel: "Zaplanuj",
        submittingLabel: "Zapisywanie...",
    };

    return (
        <div className="flex space-x-4 w-full">
            <FormComponent {...formComponentProps} />
        </div>
    );
}