open Taggen.Core
open Taggen.HtmlHelpers
open Taggen.Punctuation
open Taggen.Utils

let f1 = Text("boo")
let f2 = 
    FragAttr("p", 
        Some([("style", "background : red;")]),
        [Text "My paragraph with a red background."])
let f3 = Frag("b", [Text "boo"])
let f4 = Frag("div.fadeIn.fadeOut#myContent", [f2;f3])
let f5 x = Frag("div", 
                [
                    img_ "An image of an image." "http://example.com/image.jpg"
                    p_ "My paragraph!"
                    f4
                    f2
                    Frag("p", [x])
                ]
             )

let f6 x =
    html
    <>+ (head
        <<+ [
            ])
    <>+ (body 
        <>+ (div +. ".content"
            <<+ [
                    img_ "hello world" "/world.jpg"
                    p_ "My blue paragraph!" ++ [("style", "{ color : blue; }")]
                    p_ x +. "#actualContent"
                    span +~ "Hello world!"
                ]))

let pageFramework pageTitle navItems content =
    Frag("html",
            [
                Frag("header",
                        [Frag("title", [Text pageTitle])]
                    )
                Frag("body",
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
                Frag("li#item1.navitem", [Text "Item 1"])
                Frag("li#item2", [Text "Item 2"])
            ]
        )
    ]

do
    printfn "%s" (!!(pageFramework "My page" navItems [f5 (Text "Boo")]))
    printfn "%s" (!!(f6 "The article text?"))
    System.Console.Read () |> ignore