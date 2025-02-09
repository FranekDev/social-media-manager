"use client";

import { Button } from "@/components/ui/button";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { cn } from "@/lib/utils";
import React, { useState } from "react";
import { Loader } from "lucide-react";
import { useRouter } from "next/navigation";
import { Register, registerUser } from "@/features/user/post-create-user";
import { signIn } from "next-auth/react";
import Link from "next/link";

export default function RegisterPage() {
// debugger;
    const [isLoading, setIsLoading] = useState(false);
    const router = useRouter();

    const formSchema = z.object({
        username: z.string().min(1, {
            message: "Podaj nazwę użytkownika.",
        }),
        email: z.string().email({
            message: "Podaj poprawny adres email.",
        }),
        password: z.string().min(6, {
            message: "Hasło musi mieć co najmniej 6 znaków.",
        }),
    });

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            username: "",
            email: "",
            password: "",
        },
    });

    async function onSubmit(values: z.infer<typeof formSchema>) {
        setIsLoading(true);

        try {
            const user = await registerUser(values as Register);

            if (user) {
                const response = await signIn("credentials", {
                    username: values.username,
                    password: values.password,
                    redirect: false
                });

                if (response?.ok) {
                    router.push("/dashboard");
                } else {
                    console.error("Login failed:", response?.error);
                }
            }

        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className={cn("flex flex-col justify-center items-center w-screen h-screen w-full")}>
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
                        name="email"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel>Email</FormLabel>
                                <FormControl>
                                    <Input type="email" placeholder="Email" {...field} />
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
                                    <Input type="password" placeholder="Hasło" {...field} />
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
                                <FormLabel>Potwierdź hasło</FormLabel>
                                <FormControl>
                                    <Input type="password" placeholder="Hasło" {...field} />
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
                            Rejestracja...
                        </Button>
                    ) : (
                        <Button type="submit"
                                className={cn("w-full")}
                        >
                            Zarejestruj się
                        </Button>)}
                </form>
            </Form>
            <Button className="w-fit mt-4" variant="secondary">
                <Link href="/login">Zaloguj się</Link>
            </Button>
        </div>
    );
}