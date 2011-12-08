module Taggen.Core
open System.Text.RegularExpressions
open System.Collections

let RegMatch pattern input =
    let matches = Regex.Matches(input, pattern, RegexOptions.Compiled)
    matches |> Seq.cast |> Seq.toList
    |> List.map (fun (m : Match) ->
        [for g in m.Groups -> (g.Value)])

let inline foldStrings strings = List.fold (fun acc item -> acc + item) "" strings

let writeTagname tagText = 
    RegMatch (@"\w+") tagText
    |> List.head 
    |> List.head

let rec writeClasses tagText =
    let classes = RegMatch (@"\.\w+") tagText |> List.concat |> List.map (fun dottedName -> dottedName.Trim '.')
    match List.length classes with
    | 0 -> ""
    | 1 -> sprintf " class=\"%s\"" (List.head classes)
    | _ -> List.fold (fun acc item -> acc + " " + item) (List.head classes) (List.tail classes) |> sprintf " class=\"%s\""

let writeId tagText =
    let matches = RegMatch (@"#\w+") tagText
    if (List.length matches > 1) then failwith "An element can only have one id"
    else
        List.map (fun groups -> groups |> List.map (fun (x : string) -> sprintf " id=\"%s\"" (x.Trim('#'))) |> foldStrings) matches |> foldStrings

let writeAttr attr = 
    match attr with
    | Some m -> 
        m 
        |> List.map (fun (key, value) -> sprintf " %s=\"%s\"" key value)
        |> foldStrings
    | None -> ""

type Fragment =
    | Text of string
    | Frag of string * List<Fragment>
    | FragAttr of string * List<string * string> Option * List<Fragment> 

let rec printFrag fragment =
    let strFrag frag =
        let tag, attr, children = frag
        let tagName = writeTagname tag
        let classes = writeClasses tag
        let attrs = writeAttr attr
        let id = writeId tag
        let openContents = sprintf "%s%s%s%s" tagName id classes attrs
        let innerText = List.map printFrag children |> foldStrings
        if children = List.empty then
            sprintf "<%s/>" openContents
        else
            sprintf "<%s>%s</%s>" openContents innerText tagName

    match fragment with
    | Text t -> t
    | Frag (tag, children) ->
        strFrag (tag, None, children)
    | FragAttr (tag, attr, children) ->
        strFrag (tag, attr, children)