import {
    ControllerFieldState,
    ControllerRenderProps,
    FieldValues,
    Path,
    UseFormReturn,
    UseFormStateReturn
} from "react-hook-form";
import { ReactElement } from "react";
import { z } from "zod";

export type RenderField<T extends FieldValues> = {
    fieldName: Path<T>;
    render: ({ field, fieldState, formState }: {
        field: ControllerRenderProps<T, Path<T>>;
        fieldState: ControllerFieldState;
        formState: UseFormStateReturn<T>;
    }) => ReactElement;
};

export type FormComponentProps<T extends FieldValues> = {
    form: UseFormReturn<T>;
    schema: z.ZodObject<any>;
    renderFields: RenderField<T>[];
    onSubmit: (values: T) => void;
    submitLabel: string;
    submittingLabel: string;
};