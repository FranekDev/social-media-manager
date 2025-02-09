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
import { PenLine } from "lucide-react";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import FormComponent from "@/components/form/form-component";
import { FormComponentProps, RenderField } from "@/types/form";
import { FormControl, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Textarea } from "@/components/ui/textarea";
import React, { useEffect, useState } from "react";
import { useToast } from "@/hooks/use-toast";
import { useAuth } from "@/hooks/use-auth";
import { postInstagramCommentReply } from "@/features/instagram/api/post-instagram-comment-reply";

type AnswerCommentDialogProps = {
    updateCommentsCountAction: () => void;
    comment: string;
    commentId: string;
};

export default function AnswerCommentDialog({ updateCommentsCountAction, comment, commentId }: AnswerCommentDialogProps) {

    const [open, setOpen] = useState(false);
    const { toast } = useToast();
    const { token } = useAuth();

    const schema = z.object({
        message: z.string()
            .min(1, {
                message: "Komentarz nie może być pusty."
            }).max(2200, {
                message: "Komentarz nie może być dłuższy niż 2200 znaków."
            })
    });

    const form = useForm<z.infer<typeof schema>>({
        resolver: zodResolver(schema),
        defaultValues: {
            message: ""
        }
    });

    const { isSubmitSuccessful } = form.formState;
    const {reset} = form;

    useEffect(() => {
        if (isSubmitSuccessful) {
            reset();
        }
    }, [isSubmitSuccessful, reset]);

    const onSubmit = async (values: z.infer<typeof schema>) => {
        try {
            const {data, errors} = await postInstagramCommentReply(token, values.message, commentId);

            if (errors.length > 0) {
                errors.forEach(error => {
                    toast({
                        variant: "destructive",
                        description: error.errorMessage
                    });
                });
            }

            toast({
                description: "Odpowiedź opublikowana!"
            });

            setOpen(false);
            updateCommentsCountAction();
        }
        catch (e) {
            toast({
                variant: "destructive",
                description: "Wystąpił błąd przy publikacji komentarza"
            });
        }
    };

    const renderFields: RenderField<z.infer<typeof schema>>[] = [
        {
            fieldName: "message",
            render: ({ field }) => (
                <FormItem>
                    <FormLabel>Twoja odpowiedź</FormLabel>
                    <FormControl>
                        <Textarea
                            placeholder="Treść komentarza"
                            rows={5}
                            {...field}
                            value={field.value}
                            onChange={e => {
                                field.onChange(e.target.value);
                            }}
                        />
                    </FormControl>
                    <FormMessage/>
                </FormItem>
            )
        }
    ];

    const formProps: FormComponentProps<z.infer<typeof schema>> = {
        form,
        schema,
        onSubmit,
        renderFields,
        submitLabel: "Opublikuj komentarz",
        submittingLabel: "Publikowanie komentarza"
    };

    return (
        <Dialog open={open}
                onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <div className="w-full flex justify-center items-center">
                    <Button variant="ghost"
                            className="p-0 m-0 h-fit w-fit">
                        <PenLine size={20}/>
                    </Button>
                </div>
            </DialogTrigger>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Odpowiedz na komentarz</DialogTitle>
                    <DialogDescription>
                        Komentarz:<br/>
                        {comment}
                    </DialogDescription>
                </DialogHeader>
                <FormComponent {...formProps} />
            </DialogContent>
        </Dialog>
    );
}