"use client";

import { Button } from "@/components/ui/button";
import { signOut } from "next-auth/react";

export default function DashboardPage() {
    return (
        <div>
            <h1>Dashboard</h1>
            <Button variant={"outline"}
                    onClick={() => signOut({ redirect: false })}
            >Wyloguj</Button>
        </div>
    );
}