import { z } from "zod";
import { useToast } from "@/hooks/use-toast";
import { useAuth } from "@/hooks/use-auth";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { TikTokPhotoRequest } from "@/types/tiktok/request/tiktok-photo-request";
import { TikTokVideoRequest } from "@/types/tiktok/request/tiktok-video-request";
import { scheduleTiktokPhoto } from "@/features/tiktok/api/post-schedule-tiktok-photo";
import { scheduleTiktokVideo } from "@/features/tiktok/api/post-schedule-tiktok-video";
import { getFileBytes } from "@/lib/utils";

export type TikTokFormType = "photo" | "video";

export type PhotoFormValues = {
    title: string;
    description: string;
    images: File | null;
    scheduledAt: Date | string;
};

export type VideoFormValues = {
    title: string;
    video: File | null;
    scheduledAt: Date | string;
};

const photoSchema = z.object({
    title: z.string()
        .min(1, {
            message: "Tytuł nie może być pusty"
        }).max(90, {
            message: "Tytuł nie może być dłuższy niż 90 znaków"
        }),
    description: z.string()
        .min(1, {
            message: "Opis nie może być pusty"
        }).max(4000, {
            message: "Opis nie może być dłuższy niż 4000 znaków"
        }),
    imagesBytes: z.array(z.string()).nonempty({
        message: "Zdjęcie jest wymagane"
    }),
    scheduledAt: z.date()
});

const videoSchema = z.object({
    title: z.string().min(1, {
        message: "Tytuł nie może być pusty"
    }),
    videoBytes: z.array(z.string()).nonempty({
        message: "Film jest wymagany"
    }),
    scheduledAt: z.date()
});

export const useTikTokSchedulePostForm = (formType: TikTokFormType) => {
    const schema = formType === "photo" ? photoSchema : videoSchema;
    const { toast } = useToast();
    const { token } = useAuth();

    const form = useForm<PhotoFormValues | VideoFormValues>({
        resolver: zodResolver(schema),
        defaultValues: formType === "photo" ? {
            title: "",
            description: "",
            images: null,
            scheduledAt: new Date()
        } : {
            title: "",
            video: null,
            scheduledAt: new Date()
        }
    });

    const onSubmit = async (values: PhotoFormValues | VideoFormValues) => {
        console.log("test");
        try {
debugger;
            if (formType === "photo") {
                values = values as PhotoFormValues;

                const file = values.images as File;
                if (!(file instanceof File)) {
                    throw new TypeError("Argument is not a File object");
                }
                const base64String = await getFileBytes(file);
                const date = new Date(values.scheduledAt);
                const utcDate = date.toUTCString();

                console.log({
                    title: values.title,
                    description: values.description,
                    imagesBytes: [base64String],
                    scheduledAt: new Date(utcDate).toISOString()
                });

                const { data, errors } = await scheduleTiktokPhoto(token, {
                    title: values.title,
                    description: values.description,
                    imagesBytes: [base64String],
                    scheduledAt: new Date(utcDate).toISOString()
                });
                if (errors.length > 0) {
                    errors.forEach(error => {
                        toast({
                            title: "Wystąpił błąd",
                            variant: "destructive",
                            description: error.errorMessage
                        });
                    });
                }

                if (data != null) {
                    toast({
                        title: "Sukces",
                        description: "Post został zaplanowany"
                    });
                }

                return data;
            } else if (formType === "video") {
                debugger;
                values = values as VideoFormValues;

                const fileBase64 = await getFileBytes(values.video as File);
                const date = new Date(values.scheduledAt);
                const utcDate = date.toUTCString();

                const { data, errors } = await scheduleTiktokVideo(token, {
                    title: values.title,
                    videoBytes: fileBase64,
                    scheduledAt: new Date(utcDate).toISOString()
                });
                if (errors.length > 0) {
                    errors.forEach(error => {
                        toast({
                            variant: "destructive",
                            description: error.errorMessage
                        });
                    });
                }

                if (data != null) {
                    toast({
                        title: "Sukces",
                        description: "Post został zaplanowany"
                    });
                }
                toast({
                    title: "Sukces",
                    description: "Post został zaplanowany"
                });

                return data;
            }
        } catch (error) {
            console.log(error);
            toast({
                title: "Błąd",
                description: "Wystąpił błąd",
                variant: "destructive"
            });
        }
    };

    return { form, schema, onSubmit };
};
