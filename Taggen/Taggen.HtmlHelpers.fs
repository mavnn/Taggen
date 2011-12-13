module Taggen.HtmlHelpers
open Taggen.Core
open Taggen.Punctuation

/// <summary>Image tag helper</summary>
/// <param name="alt">Alternative text</param>
/// <param name="source">Location of image file</param>
let img_ alt source =
    FragAttr ("img", (Some ["src", source; "alt", alt]), [])

/// Creates a paragraph tag with inner text of 'text'
let p_ text =
    Frag ("p", [Text text])

// Link helpers
let linkTo text location =
    FragAttr ("a", Some([("href", location)]), [Text text])

let mailTo email =
    FragAttr ("a", Some([("href", email)]), [Text email])

// 'Container tags' helpers - these are tags that some browers complain about if they don't have explicit closing tags.
let private container tagName =
    !<> tagName ^<> Text ""

let a = container "a"
let b = container "b"
let body = container "body"
let canvas = container "canvas"
let dd = container "dd"
let div = container "div"
let dl = container "dl"
let dt = container "dt"
let em = container "em"
let fieldset = container "fieldset"
let form = container "form"
let h1 = container "h1"
let h2 = container "h2"
let h3 = container "h3"
let h4 = container "h4"
let h5 = container "h5"
let h6 = container "h6"
let head = container "head"
let html = container "html"
let i = container "i"
let iframe = container "iframe"
let label = container "label"
let li = container "li"
let ol = container "ol"
let option = container "option"
let pre = container "pre"
let script = container "script"
let span = container "span"
let strong = container "strong"
let style = container "style"
let table = container "table"
let textarea = container "textarea"
let ul = container "ul"
