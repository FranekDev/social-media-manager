import { FieldValues } from "react-hook-form";
import { Form, FormField } from "@/components/ui/form";
import { FormComponentProps } from "@/types/form";
import { Button } from "@/components/ui/button";
import { Loader } from "lucide-react";
import React from "react";

export default function FormComponent<T extends FieldValues>(
    { form, renderFields, onSubmit, submitLabel, submittingLabel }: FormComponentProps<T>
) {
    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)}
                  className="space-y-4 w-full">
                {renderFields.map(({ fieldName, render }) => (
                    <FormField
                        render={({ field, fieldState, formState }) => render({ field, fieldState, formState })}
                        name={fieldName}
                        control={form.control}
                        key={fieldName.toString()}
                    />
                ))}

                {form.formState.isSubmitting ? (
                    <Button className={"w-full"}
                            disabled>
                        <Loader className={"w-6 h-6 mr-2 animate-spin"}/>
                        {submittingLabel}
                    </Button>
                ) : (
                    <Button type="submit"
                            className={"w-full"}>
                        {submitLabel}
                    </Button>
                )}

            </form>
        </Form>
    );
}