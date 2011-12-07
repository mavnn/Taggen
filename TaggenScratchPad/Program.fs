open Taggen

let f1 = Text("boo")
let f2 = Frag("p", Seq.empty)
let f3 = Frag("b", [Text("boo")])
let f4 = Frag("div.fadeIn.fadeOut#myContent", [f2;f3])
let f5 x = Frag("div", 
                [
                    f4
                    f2
                    Frag("p", [x])
                ]
             )

let pageFramework pageTitle navItems content =
    Frag("html",
            [
                Frag("Header",
                        [Frag("Title", [Text(pageTitle)])]
                    )
                Frag("Body",
                        [
                            Frag("nav", navItems)
                            Frag("content", content)
                        ]
                    )
            ]
        )

let navItems =
    [
        Frag("ul#navList",
            [
                Frag("li#item1", [Text("Item 1")])
                Frag("li#item2", [Text("Item 2")])
            ]
        )
    ]

do
    printfn "%s" (printFrag (pageFramework "My page" navItems [(f5 (Text "Boo"))]))
    System.Console.Read () |> ignore