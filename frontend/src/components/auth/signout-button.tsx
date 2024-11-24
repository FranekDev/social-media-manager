"use client";

import { Button } from "@/components/ui/button";
import { signOut } from "next-auth/react";

export default function SignOutButton() {
    return <Button variant="link"
                   onClick={() =>
                       signOut({ redirect: false })
                   }
    >
        Wyloguj
    </Button>;
}