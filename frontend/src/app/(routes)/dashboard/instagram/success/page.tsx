"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/hooks/use-auth";

export default function InstagramSuccessPage() {
    const router = useRouter();
    const {token} = useAuth();

    useEffect(() => {
        const hash = window.location.hash.substring(1);
        const params = new URLSearchParams(hash);
        const accessToken = params.get("access_token");

        if (accessToken) {
            fetch("https://localhost:7114/api/InstagramUser", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
                body: JSON.stringify({ token: accessToken }),
            })
                .then(response => {
                    if (response.ok) {
                        router.push("/dashboard/instagram");
                    } else {
                        console.error("Failed to send token");
                    }
                })
                .catch(error => {
                    console.error("Error:", error);
                });
        }
    }, [router, token]);

    return (
        <div className="w-full h-full flex justify-center items-center">
            <h1>Konto Instagram zostało połączone</h1>
        </div>
    );
}