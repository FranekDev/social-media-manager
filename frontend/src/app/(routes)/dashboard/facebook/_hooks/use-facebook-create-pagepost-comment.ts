import { z } from "zod";
import { useToast } from "@/hooks/use-toast";
import { useAuth } from "@/hooks/use-auth";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { createPagepostComment } from "@/features/facebook/api/post-create-pagepost-comment";

type FormValues = {
    message: string;
};

const schema = z.object({
    message: z.string().max(63206, {
        message: "Opis nie może być dłuższy niż 2200 znaków"
    }),
});

export const useFacebookCreatePagePostCommentForm = () => {
    const { toast } = useToast();
    const { token } = useAuth();

    const form = useForm<FormValues>({
        resolver: zodResolver(schema),
        defaultValues: {
            message: ""
        },
    });

    const onSubmit = async (values: { message: string, pagePostId: string, postCommentId: string }) => {
        try {
            const { data, errors } = await createPagepostComment(token, values);
            if (errors?.length > 0) {
                errors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.message
                    });
                });
            } else {
                toast({
                    description: "Komentarz został dodany"
                });
            }
        } catch (error: any) {
            toast({
                variant: "destructive",
                description: error.message
            });
        }
    };

    return {
        form,
        schema,
        onSubmit
    };
};