"use client";

import { useSession } from "next-auth/react";
import { ChevronUp, User2 } from "lucide-react";

export default function UserInfo() {
    const { data: session } = useSession();

    return (
        <>
            <User2/>{ session?.user?.token?.username || "User" }
            <ChevronUp className="ml-auto"/>
        </>
    );
}