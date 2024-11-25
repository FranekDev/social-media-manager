"use client";

import { ChevronUp, User2 } from "lucide-react";
import { useAuth } from "@/hooks/use-auth";

export default function UserInfo() {
    const { user } = useAuth();

    return (
        <>
            <User2/>{user?.username || "User"}
            <ChevronUp className="ml-auto"/>
        </>
    );
}