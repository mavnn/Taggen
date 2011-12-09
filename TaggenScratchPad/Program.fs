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
                    img "An image of an image." "http://example.com/image.jpg"
                    p "My paragraph!"
                    f4
                    img "Another image" "http://example.com/image2.jpg" +. ".right"
                    f2
                    p "A paragraph with class and id!" +. "#withIdAndClass.myClass2"
                    p "With random attributes." ++ [("randomAttr", "withRandomValue")]
                    Frag("p", [x])
                    !<> "p"
                ]
             )

let f6 x =
    !<> "html"
    <<< (!<> "body"
            <<< !<> "div" +. ".content"
            <<< img "hello world" "/world.jpg"
            <<< p "My blue paragraph!" ++ [("style", "{ color : blue; }")]
            <<< p x +. "#actualContent")

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