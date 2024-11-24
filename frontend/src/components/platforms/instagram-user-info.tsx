"use client";

import { api } from "@/lib/api-client";
import { API_PATHS } from "@/configurations/api-paths";
import { useSession } from "next-auth/react";
import { useEffect, useState } from "react";

export default function InstagramUserInfo() {

    const { data: session } = useSession();
    const [igUser, setIgUser] = useState<any>(null);

    useEffect(() => {
        const fetchIgUser = async () => {
            if (session) {
                const user = await api.call(API_PATHS.instagram.getUser, {
                    headers: {
                        Authorization: `Bearer ${session?.user.token.token}`,
                    },
                });
                setIgUser(user);
            }
        };
        fetchIgUser().catch(console.error);
    }, [session]);


    return (
        <div>
            <h1>Instagram</h1>
        </div>
    );
}