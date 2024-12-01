"use client";

import {
    InstagramFormType,
    useInstagramSchedulePostForm
} from "@/app/(routes)/dashboard/instagram/_hooks/use-instagram-schedule-post-form";
import React, { useMemo, useState } from "react";
import { FormComponentProps, RenderField } from "@/types/form";
import { FormControl, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Textarea } from "@/components/ui/textarea";
import { Input } from "@/components/ui/input";
import { Controller } from "react-hook-form";
import { DatePicker } from "@nextui-org/date-picker";
import { getLocalTimeZone, now, today } from "@internationalized/date";
import FormComponent from "@/components/form/form-component";
import { z } from "zod";
import PreviewContent from "@/app/(routes)/dashboard/instagram/_components/preview-content";
import { useToast } from "@/hooks/use-toast";

type ScheduleContentProps = {
    type: InstagramFormType;
}

export default function ScheduleContent({ type }: ScheduleContentProps) {
    const { schema, form, onSubmit } = useInstagramSchedulePostForm(type);
    const { toast } = useToast();

    type PostFormValues = z.infer<typeof schema>;
    const [previewData, setPreviewData] = useState<PostFormValues>({
        caption: "",
        file: null,
        scheduledAt: ""
    });

    const fileUrl = useMemo(() =>
            previewData.file ? URL.createObjectURL(previewData.file) : ""
        , [previewData.file]);

    const renderFields: RenderField<PostFormValues>[] = [
        {
            fieldName: "caption",
            render: ({ field }) => (
                <FormItem>
                    <FormLabel>Opis</FormLabel>
                    <FormControl>
                        <Textarea
                            placeholder="Opis postu na Instagrama"
                            rows={5}
                            {...field}
                            value={typeof field.value === "string" ? field.value : ""}
                            onChange={e => {
                                field.onChange(e.target.value);
                                setPreviewData({ ...previewData, caption: e.target.value });
                            }}
                        />
                    </FormControl>
                    <FormMessage/>
                </FormItem>
            )
        },
        {
            fieldName: "file",
            render: ({ field: { value, onChange, ...fieldProps } }) => (
                <FormItem>
                    <FormLabel>{type === "post" ? "Zdjęcie" : "Film"}</FormLabel>
                    <FormControl>
                        {type === "post" ? (
                            <Input id="photo"
                                   type="file" {...fieldProps}
                                   onChange={(event) => {
                                       const file = event.target.files && event.target.files[0];
                                       onChange(file);
                                       setPreviewData({ ...previewData, file });
                                   }}
                            />
                        ) : (
                            <Input
                                id="video"
                                type="file"
                                accept="video/mp4"
                                {...fieldProps}
                                onChange={(event) => {
                                    const file = event.target.files && event.target.files[0];
                                    onChange(file);
                                    setPreviewData({ ...previewData, file });
                                }}
                            />
                        )}
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
                                    setPreviewData({ ...previewData, scheduledAt: dateString });
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
        }
    ];

    const formComponentProps: FormComponentProps<PostFormValues> = {
        form,
        schema,
        renderFields,
        onSubmit,
        submitLabel: "Zaplanuj",
        submittingLabel: "Zapisywanie...",
    };

    return (
        <div className="flex space-x-4 w-full">
            <FormComponent {...formComponentProps} />
            {previewData.file && <PreviewContent type={type}
                                                 fileUrl={fileUrl}
                                                 caption={previewData.caption}/>}
        </div>
    );
}