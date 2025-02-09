"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/hooks/use-auth";

export default function FacebookSuccessPage() {
    const router = useRouter();
    const {token} = useAuth();

    useEffect(() => {
        const hash = window.location.hash.substring(1);
        const params = new URLSearchParams(hash);
        const accessToken = params.get("access_token");

        if (accessToken) {
            fetch("https://localhost:7114/api/FacebookUser", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
                body: JSON.stringify({ token: accessToken }),
            })
                .then(response => {
                    if (response.ok) {
                        router.push("/dashboard/facebook");
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
            <h1>Konto Facebook zostało połączone</h1>
        </div>
    );
}