"use client";

import { Button } from "@/components/ui/button";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { cn } from "@/lib/utils";
import { signIn } from "next-auth/react";
import React, { useState } from "react";
import { Loader } from "lucide-react";
import { useRouter, useSearchParams } from "next/navigation";

export default function LoginPage() {

    const [isLoading, setIsLoading] = useState(false);
    const router = useRouter();
    const searchParams = useSearchParams();
    const callbackUrl = searchParams.get('callbackUrl') || '/dashboard';

    const formSchema = z.object({
        username: z.string().min(1, {
            message: "Podaj nazwę użytkownika.",
        }),
        password: z.string().min(1, {
            message: "Podaj hasło.",
        }),
    });

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            username: "",
            password: "",
        },
    });

    async function onSubmit(values: z.infer<typeof formSchema>) {
        setIsLoading(true);

        try {
            const response = await signIn("credentials", {
                username: values.username,
                password: values.password,
                redirect: false
            });

            if (response?.ok) {
                router.push(callbackUrl);
            } else {
                console.error("Login failed:", response?.error);
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className={cn("flex justify-center items-center w-screen h-screen")}>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)}
                      className="space-y-8">
                    <FormField
                        control={form.control}
                        name="username"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Nazwa użytkownika</FormLabel>
                                <FormControl>
                                    <Input placeholder="Nazwa użytkownika" {...field} />
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="password"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Hasło</FormLabel>
                                <FormControl>
                                    <Input type="password"
                                           placeholder="Hasło" {...field} />
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                    {isLoading ? (
                        <Button
                            className={cn("w-full")}
                            disabled
                        >
                            <Loader className={cn("w-6 h-6 mr-2 animate-spin")}/>
                            Logowanie...
                        </Button>
                    ) : (
                        <Button type="submit"
                                className={cn("w-full")}
                        >
                            Zaloguj się
                        </Button>)}
                </form>
            </Form>
        </div>
    );
}