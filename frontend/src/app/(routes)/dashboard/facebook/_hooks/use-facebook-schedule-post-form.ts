import { z } from "zod";
import { useToast } from "@/hooks/use-toast";
import { useAuth } from "@/hooks/use-auth";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { scheduleFacebookPost } from "@/features/facebook/api/post-schedule-facebook-post";

type FormValues = {
    message: string;
    scheduledAt: string;
    pageId: string;
};

const schema = z.object({
    message: z.string().max(63206, {
        message: "Opis nie może być dłuższy niż 2200 znaków"
    }),
    scheduledAt: z.string().refine(dateString => {
        const date = new Date(dateString);
        return date > new Date();
    }, {
        message: "Zaplanowana data nie może być wcześniejsza niż dzisiaj"
    }),
    pageId: z.string()
});

export const useFacebookSchedulePostForm = () => {
    const { toast } = useToast();
    const { token } = useAuth();

    const form = useForm<FormValues>({
        resolver: zodResolver(schema),
        defaultValues: {
            message: "",
            scheduledAt: "",
            pageId: ""
        },
    });

    const onSubmit = async (values: FormValues) => {
        try {
            const date = new Date(values.scheduledAt);
            const utcDate = date.toUTCString();
            const { data, errors } = await scheduleFacebookPost(token, {
                    ...values,
                    scheduledAt: new Date(utcDate).toISOString()
                }
            );
            if (errors?.length > 0) {
                errors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.message
                    });
                });
            } else {
                toast({
                    description: "Post został zaplanowany"
                });
            }
        } catch (error: any) {
            toast({
                variant: "destructive",
                description: "Wystąpił błąd podczas planowania postu"
            });
        }
    };

    return { schema, form, onSubmit };
}
