"use client";

import {
    PhotoFormValues,
    TikTokFormType,
    useTikTokSchedulePostForm, VideoFormValues
} from "@/app/(routes)/dashboard/tiktok/_hooks/use-tiktok-schedule-post-form";
import React, { useMemo, useState } from "react";
import { z } from "zod";
import { FormControl, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Controller, UseFormReturn } from "react-hook-form";
import { DatePicker } from "@nextui-org/date-picker";
import { getLocalTimeZone, now, today } from "@internationalized/date";
import { FormComponentProps, RenderField } from "@/types/form";
import FormComponent from "@/components/form/form-component";
import PreviewContent from "@/app/(routes)/dashboard/tiktok/_components/preview-content";

type ScheduleContentProps = {
    type: TikTokFormType;
}

export default function ScheduleContent({ type }: ScheduleContentProps) {

    const { form, schema, onSubmit } = useTikTokSchedulePostForm(type);

    const [photoPreviewData, setPhotoPreviewData] = useState<PhotoFormValues>({
        title: "",
        description: "",
        images: null,
        scheduledAt: new Date()
    });

    const [videoPreviewData, setVideoPreviewData] = useState<VideoFormValues>({
        title: "",
        video: null,
        scheduledAt: new Date()
    });

    const handleSubmit = async (values: PhotoFormValues | VideoFormValues) => {
        //  validate form
        //const isValid = await form.trigger();
       // if (!isValid) return;

        await onSubmit(values);
    }

    const photoFields: RenderField<PhotoFormValues>[] = [
        {
            fieldName: "title",
            render: ({ field }) => (
                <FormItem>
                    <FormLabel>Tytuł</FormLabel>
                    <FormControl>
                        <Input placeholder="Tytuł postu na TikToka"
                               {...field}
                               value={typeof field.value === "string" ? field.value : ""}
                               onChange={(e) => {
                                   field.onChange(e.target.value);
                                   setPhotoPreviewData((prevData) => ({
                                       ...prevData,
                                       title: e.target.value
                                   }));
                               }}
                        />
                    </FormControl>
                    <FormMessage />
                </FormItem>
            )
        },
        {
            fieldName: "description",
            render: ({ field }) => (
                <FormItem>
                    <FormLabel>Opis</FormLabel>
                    <FormControl>
                        <Textarea placeholder="Opis postu na TikToka"
                                  rows={5}
                                  {...field}
                                  value={typeof field.value === "string" ? field.value : ""}
                                  onChange={(e) => {
                                      field.onChange(e.target.value);
                                      setPhotoPreviewData((prevData) => ({
                                          ...prevData,
                                          description: e.target.value
                                      }));
                                  }}
                        />
                    </FormControl>
                    <FormMessage />
                </FormItem>
            )
        },
        {
            fieldName: "images",
            render: ({ field: { value, onChange, ...fieldProps } }) => (
                <FormItem>
                    <FormLabel>Zdjęcie</FormLabel>
                    <FormControl>
                        <Input id="photo"
                               type="file"
                               {...fieldProps}
                               onChange={(event) => {
                                   const file = event.target.files && event.target.files[0];
                                   onChange(file);
                                   console.log(file);
                                   setPhotoPreviewData((prevData) => ({
                                       ...prevData,
                                       images: file
                                   }));
                               }}
                        />
                    </FormControl>
                    <FormMessage />
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
                                    setPhotoPreviewData((prevData) => ({
                                        ...prevData,
                                        scheduledAt: new Date(dateString)
                                    }));
                                }}
                                label="Data publikacji"
                                labelPlacement="outside"
                                isRequired={true}
                            />
                        )}
                    />
                    <FormMessage />
                </FormItem>
            )
        }
    ];

    const videoFields: RenderField<VideoFormValues>[] = [
        {
            fieldName: "title",
            render: ({ field }) => (
                <FormItem>
                    <FormLabel>Tytuł</FormLabel>
                    <FormControl>
                        <Input placeholder="Tytuł postu na TikToka"
                               {...field}
                               value={typeof field.value === "string" ? field.value : ""}
                               onChange={(e) => {
                                   field.onChange(e.target.value);
                                   setVideoPreviewData((prevData) => ({
                                       ...prevData,
                                       title: e.target.value
                                   }));
                               }}
                        />
                    </FormControl>
                    <FormMessage/>
                </FormItem>
            )
        },
        {
            fieldName: "video",
            render: ({ field: { value, onChange, ...fieldProps } }) => (
                <FormItem>
                    <FormLabel>Film</FormLabel>
                    <FormControl>
                        <Input id="video"
                               type="file"
                               accept="video/mp4"
                               {...fieldProps}
                               onChange={(event) => {
                                   const file = event.target.files && event.target.files[0];
                                   onChange(file);
                                   setVideoPreviewData((prevData) => ({
                                       ...prevData,
                                       video: file
                                   }));
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
                                className="max-w-2xl w-full"
                                minValue={today(getLocalTimeZone())}
                                defaultValue={now("Europe/Warsaw")}
                                onChange={(date) => {
                                    const dateString = date.toString().split("[")[0];
                                    onChange(dateString);
                                    setVideoPreviewData((prevData) => ({
                                        ...prevData,
                                        scheduledAt: new Date(dateString)
                                    }));
                                }}
                                label="Data publikacji"
                                labelPlacement="outside"
                                isRequired={true}
                            />
                        )}
                    />
                    <FormMessage />
                </FormItem>
            )
        }
    ];

    const photoFormComponentProps: FormComponentProps<PhotoFormValues> = {
        form: form as UseFormReturn<PhotoFormValues>,
        schema,
        renderFields: photoFields,
        onSubmit: handleSubmit,
        submitLabel: "Zaplanuj post",
        submittingLabel: "Planowanie posta...",
    };

    const videoFormComponentProps: FormComponentProps<VideoFormValues> = {
        form: form as UseFormReturn<VideoFormValues>,
        schema,
        renderFields: videoFields,
        onSubmit: handleSubmit,
        submitLabel: "Zaplanuj post",
        submittingLabel: "Planowanie posta...",
    };

    return (
        <div className="flex space-x-4 w-full">
            {type === "photo" ? (
                <FormComponent {...photoFormComponentProps} />
            ) : (
                <FormComponent {...videoFormComponentProps} />
            )}

            {type === "photo" && photoPreviewData.images && (
                <PreviewContent type={type} data={photoPreviewData} />
            )}
            {type === "video" && videoPreviewData.video && (
                <PreviewContent type={type} data={videoPreviewData} />
            )}
        </div>
    );
}