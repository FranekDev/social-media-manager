import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";

type TabsContentProps = {
    tabs: TabContent[];
};

export default function TabsView({ tabs }: TabsContentProps) {
    return (
        <Tabs defaultValue={tabs[0].value}>
            <TabsList>
                {tabs.map((tab) => (
                    <TabsTrigger key={tab.value} value={tab.value}>
                        {tab.title}
                    </TabsTrigger>
                ))}
            </TabsList>
            {tabs.map((tab) => (
                <TabsContent key={tab.value} value={tab.value}>
                    {tab.content}
                </TabsContent>
            ))}
        </Tabs>
    );
}