"use client";

import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { useEffect, useState } from "react";
import { useAuth } from "@/hooks/use-auth";

export default function InstagramUserInfo() {

    const { token } = useAuth();
    const [igUser, setIgUser] = useState<any>(null);

    useEffect(() => {
        const fetchIgUser = async () => {
            const user = await api.call(API_PATHS.instagram.getUser, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            setIgUser(user);
        };
        fetchIgUser().catch(console.error);
    }, [token]);


    return (
        <div>
            <h1>Instagram</h1>
        </div>
    );
}