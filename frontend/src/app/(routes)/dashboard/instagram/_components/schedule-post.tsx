"use client";

import { FormControl, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Textarea } from "@/components/ui/textarea";
import { Input } from "@/components/ui/input";
import { Controller, useForm } from "react-hook-form";
import { DatePicker } from "@nextui-org/date-picker";
import { getLocalTimeZone, now, today } from "@internationalized/date";
import React, { useMemo, useState } from "react";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { API_PATHS } from "@/configurations/api-paths";
import { api } from "@/lib/api-client";
import FormComponent from "@/components/form/form-component";
import { FormComponentProps, RenderField } from "@/types/form";
import { useToast } from "@/hooks/use-toast";
import Image from "next/image";
import { scheduleInstagramPost } from "@/features/instagram/api/post-schedule-instagram-post";
import { useSession } from "next-auth/react";
import { getFileBytes } from "@/lib/utils";

export default function SchedulePost() {
    const { toast } = useToast();
    const { data: session } = useSession();

    const schema = z.object({
        caption: z.string().max(2200, {
            message: "Opis nie może być dłuższy niż 2200 znaków"
        }),
        file: z.instanceof(File).nullable().refine(file => file.size > 0, {
            message: "Zdjęcie jest wymagane"
        }),
        scheduledAt: z.string().refine(dateString => {
            const date = new Date(dateString);
            return date > new Date();
        }, {
            message: "Zaplanowana data nie może być wcześniejsza niż dzisiaj"
        })
    });

    const form = useForm<z.infer<typeof schema>>({
        resolver: zodResolver(schema),
        defaultValues: {
            caption: "",
            file: null,
            scheduledAt: ""
        },
    });

    type PostFormValues = z.infer<typeof schema>;
    const [previewData, setPreviewData] = useState<PostFormValues>({
        caption: "",
        file: null,
        scheduledAt: ""
    });

    const imageUrl = useMemo(() =>
            previewData.file ? URL.createObjectURL(previewData.file) : ""
        , [previewData.file]);

    async function onSubmit(values: z.infer<typeof schema>) {
        try {
            const date = new Date(values.scheduledAt);
            const utcDate = date.toUTCString();
            const imageBase64 = await getFileBytes(values.file as File);

            const response = await scheduleInstagramPost(session?.user.token.token as string, {
                imageBytes: imageBase64.toString(),
                caption: values.caption.toString(),
                scheduledAt: new Date(utcDate).toISOString()
            });

            toast({
                title: "Sukces",
                description: "Post został zaplanowany"
            });
        } catch (error) {
            toast({
                title: "Błąd",
                description: "Nie udało się zaplanować postu",
                variant: "destructive"
            });
        }
    }

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
                    <FormLabel>Zdjęcie</FormLabel>
                    <FormControl>
                        <Input id="photo"
                               type="file" {...fieldProps}
                               onChange={(event) => {
                                   const file = event.target.files && event.target.files[0];
                                   onChange(file);
                                   setPreviewData({ ...previewData, file });

                               }}
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
                                className="max-w-xs"
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
        },
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
        <div className="flex space-x-4 w-fit">
            <FormComponent {...formComponentProps} />
            {previewData.file && (
                <div className="w-fit h-full flex flex-col space-y-2">
                    <h2>Podgląd</h2>
                    <Image src={imageUrl}
                           alt="Podgląd zdjęcia"
                           width={250}
                           height={500}
                           className="rounded-sm"
                    />
                    <p className="max-w-60 text-wrap break-words">{previewData.caption}</p>
                </div>
            )}
        </div>
    );
}