[<AutoOpen>]
module Taggen.Utils
open Taggen.Core
open System.IO
open System.Xml

let prettyPrint frag =
    let stringRep = printFrag frag
    let doc = new XmlDocument ()
    doc.LoadXml(stringRep)
    let mem = new MemoryStream ()
    let xmlWriter = new XmlTextWriter (mem, System.Text.Encoding.UTF8)
    xmlWriter.Formatting <- Formatting.Indented
    doc.WriteTo xmlWriter
    xmlWriter.Flush ()
    mem.Seek (int64 0, SeekOrigin.Begin) |> ignore
    let reader = new StreamReader (mem)
    reader.ReadToEnd ()


/// Add child to fragment (as last child)
let addChild child fragment =
    match fragment with
    | FragAttr (tag, attr, children) -> FragAttr (tag, attr, List.append children [child])
    | _ -> failwith "You cannot add a child fragment to a Text fragment."

/// Add inner text to fragment
let addText text fragment =
    addChild (Text text) fragment

/// Add list of children to fragment
let addChildList newChildren fragment =
    match fragment with
    | FragAttr (tag, attr, children) -> FragAttr (tag, attr, List.append children newChildren)
    | _ -> failwith "You cannot add children to a Text fragment."

/// Add classes or id to existing fragment using ".class#id" syntax
let addClassOrId classes fragment =
    match fragment with
    | FragAttr (tag, attr, children) ->
        updateAttr fragment (unpackClasses classes)
    | _ -> failwith "You cannot add classes or id to a Text fragment."
