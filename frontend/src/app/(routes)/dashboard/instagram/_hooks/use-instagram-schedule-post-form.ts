import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { getFileBytes } from "@/lib/utils";
import { scheduleInstagramPost } from "@/features/instagram/api/post-schedule-instagram-post";
import { useToast } from "@/hooks/use-toast";
import { scheduleInstagramReel } from "@/features/instagram/api/post-schedule-instagram-reel";
import { useAuth } from "@/hooks/use-auth";

export type InstagramFormType = "post" | "reel";

type PostFormValues = {
    caption: string;
    file: File | null;
    scheduledAt: string;
};

const postSchema = z.object({
    caption: z.string().max(2200, {
        message: "Opis nie może być dłuższy niż 2200 znaków"
    }),
    file: z.instanceof(File).nullable()
        .refine(file => file && ["image/png", "image/jpeg"].includes(file.type), {
            message: "Zdjęcie musi być w formacie PNG lub JPG"
        })
        .refine(file => file != null && file.size > 0, {
            message: "Zdjęcie jest wymagane"
        }),
    scheduledAt: z.string().refine(dateString => {
        const date = new Date(dateString);
        return date > new Date();
    }, {
        message: "Zaplanowana data nie może być wcześniejsza niż dzisiaj"
    })
});

const reelSchema = z.object({
    caption: z.string().max(2200, {
        message: "Opis nie może być dłuższy niż 2200 znaków"
    }),
    file: z.instanceof(File).nullable()
        .refine(file => file && ["video/mp4"].includes(file.type), {
            message: "Film musi być w formacie MP4"
        })
        .refine(file => file != null && file.size > 0, {
            message: "Film jest wymagany"
        }),
    scheduledAt: z.string().refine(dateString => {
        const date = new Date(dateString);
        return date > new Date();
    }, {
        message: "Zaplanowana data nie może być wcześniejsza niż dzisiaj"
    })
});

export const useInstagramSchedulePostForm = (formType: InstagramFormType) => {
    const schema = formType === "post" ? postSchema : reelSchema;
    const { toast } = useToast();
    const { token } = useAuth();

    const form = useForm<PostFormValues>({
        resolver: zodResolver(schema),
        defaultValues: {
            caption: "",
            file: null,
            scheduledAt: ""
        },
    });

    const onSubmit = async (data: PostFormValues) => {
        try {
            const date = new Date(data.scheduledAt);
            const utcDate = date.toUTCString();
            const fileBase64 = await getFileBytes(data.file as File);

            let message: string | null = null;
            if (formType === "post") {
                const response = await scheduleInstagramPost(token, {
                    imageBytes: fileBase64,
                    caption: data.caption,
                    scheduledAt: new Date(utcDate).toISOString()
                });
                message = "Post został zaplanowany";
            } else if (formType === "reel") {
                const response = await scheduleInstagramReel(token, {
                    videoBytes: fileBase64,
                    caption: data.caption,
                    scheduledAt: new Date(utcDate).toISOString()
                });
                message = "Rolka została zaplanowana";
            }

            if (message != null) {
                toast({
                    title: "Sukces",
                    description: message
                });
            }
        } catch (error) {
            toast({
                title: "Błąd",
                description: `Nie udało się zaplanować ${formType === "post" ? "posta" : "rolki"}`,
                variant: "destructive"
            });
        }
    };

    return { schema, form, onSubmit };
};