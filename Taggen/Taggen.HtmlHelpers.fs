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

// Other tags
let abbr = Frag ("abbr", [])
let acronym = Frag ("acronym", [])
let address = Frag ("address", [])
let applet = Frag ("applet", [])
let area = Frag ("area", [])
let article = Frag ("article", [])
let aside = Frag ("aside", [])
let audio = Frag ("audio", [])
let base_ = Frag ("base", [])
let basefont = Frag ("basefont", [])
let bdo = Frag ("bdo", [])
let big = Frag ("big", [])
let blockquote = Frag ("blockquote", [])
let br = Frag ("br", [])
let button = Frag ("button", [])
let caption = Frag ("caption", [])
let center = Frag ("center", [])
let cite = Frag ("cite", [])
let code = Frag ("code", [])
let col = Frag ("col", [])
let colgroup = Frag ("colgroup", [])
let command = Frag ("command", [])
let datalist = Frag ("datalist", [])
let del = Frag ("del", [])
let details = Frag ("details", [])
let dialog = Frag ("dialog", [])
let dfn = Frag ("dfn", [])
let dir = Frag ("dir", [])
let embed = Frag ("embed", [])
let figure = Frag ("figure", [])
let font = Frag ("font", [])
let footer = Frag ("footer", [])
let frame = Frag ("frame", [])
let frameset = Frag ("frameset", [])
let header = Frag ("header", [])
let hgroup = Frag ("hgroup", [])
let hr = Frag ("hr", [])
let img = Frag ("img", [])
let input = Frag ("input", [])
let ins = Frag ("ins", [])
let keygen = Frag ("keygen", [])
let kbd = Frag ("kbd", [])
let legend = Frag ("legend", [])
let link = Frag ("link", [])
let map = Frag ("map", [])
let mark = Frag ("mark", [])
let menu = Frag ("menu", [])
let meta = Frag ("meta", [])
let meter = Frag ("meter", [])
let nav = Frag ("nav", [])
let noframes = Frag ("noframes", [])
let noscript = Frag ("noscript", [])
let object_ = Frag ("object", [])
let optgroup = Frag ("optgroup", [])
let output = Frag ("output", [])
let p = Frag ("p", [])
let param = Frag ("param", [])
let progress = Frag ("progress", [])
let q = Frag ("q", [])
let rp = Frag ("rp", [])
let samp = Frag ("samp", [])
let section = Frag ("section", [])
let select = Frag ("select", [])
let small = Frag ("small", [])
let source = Frag ("source", [])
let strike = Frag ("strike", [])
let sub = Frag ("sub", [])
let sup = Frag ("sup", [])
let tbody = Frag ("tbody", [])
let td = Frag ("td", [])
let tfoot = Frag ("tfoot", [])
let th = Frag ("th", [])
let thead = Frag ("thead", [])
let time = Frag ("time", [])
let title = Frag ("title", [])
let tr = Frag ("tr", [])
let tt = Frag ("tt", [])
let u = Frag ("u", [])
let var = Frag ("var", [])
let video = Frag ("video", [])
let xmp = Frag ("xmp", [])