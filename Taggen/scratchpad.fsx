#load "Taggen.Core.fs"
#load "Taggen.Utils.fs"
#load "Taggen.Operators.fs"
#load "Taggen.HtmlHelpers.fs"

open Taggen.Core
open Taggen.HtmlHelpers
open Taggen.Operators
open Taggen.Utils

let f1 = Text("boo")
let f2 =
    FragAttr("p",
        [("style", "background : red;")],
        [Text "My paragraph with a red background."])
let f3 = Frag("b.bold", [Text "boo"])
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

let frameWork pageTitle contentFrag =
    Frag("html",
        [
            Frag("head", [Frag("title", [Text pageTitle])])
            Frag("body",
                [
                    Frag("nav",
                        [
                            Frag("ul#navItems",
                                [
                                    Frag("li#homeLink.navItem",
                                        [
                                            FragAttr("a", ["href", "/"], [Text "Home"])
                                        ])
                                ])
                        ])
                    Frag("div#content", contentFrag)
                ])
        ])

let conciseFramework pageTitle contentFrag =
    html
    +<> (head +<> title % pageTitle)
    +<> (body
        +<> (nav
            +<> (ul %. "#navItems"
                +<> (li %. "#homeLink.navItem" +<> linkTo "Home" "/")))
        +<> (div %. "#content"
            +<< contentFrag))

