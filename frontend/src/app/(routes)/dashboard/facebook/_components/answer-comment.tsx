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
import FormComponent from "@/components/form/form-component";
import { FormComponentProps, RenderField } from "@/types/form";
import { FormControl, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Textarea } from "@/components/ui/textarea";
import React, { useEffect, useState } from "react";
import {
    useFacebookCreatePagePostCommentForm
} from "@/app/(routes)/dashboard/facebook/_hooks/use-facebook-create-pagepost-comment";
import { z } from "zod";

type AnswerCommentDialogProps = {
    comment: string;
    commentId: string;
    pagePostId: string;
};

export default function AnswerComment({ pagePostId, comment, commentId }: AnswerCommentDialogProps) {
    const [open, setOpen] = useState(false);
    const { form, schema, onSubmit } = useFacebookCreatePagePostCommentForm();

    const { isSubmitSuccessful } = form.formState;
    const { reset } = form;

    useEffect(() => {
        if (isSubmitSuccessful) {
            reset();
        }
    }, [isSubmitSuccessful, reset]);

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
                    <FormMessage />
                </FormItem>
            )
        }
    ];

    const submit = async (values: z.infer<typeof schema>) => {
        const body = {
            message: values.message,
            pagePostId: pagePostId,
            postCommentId: commentId
        };
        await onSubmit(body);
    }

    const formProps: FormComponentProps<z.infer<typeof schema>> = {
        form,
        schema,
        onSubmit: submit,
        renderFields,
        submitLabel: "Opublikuj komentarz",
        submittingLabel: "Publikowanie komentarza"
    };

    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <div className="w-full flex justify-center items-center">
                    <Button variant="ghost" className="p-0 m-0 h-fit w-fit">
                        <PenLine size={20} />
                    </Button>
                </div>
            </DialogTrigger>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Odpowiedz na komentarz</DialogTitle>
                    <DialogDescription>
                        Komentarz:<br />
                        {comment}
                    </DialogDescription>
                </DialogHeader>
                <FormComponent {...formProps} />
            </DialogContent>
        </Dialog>
    );
}