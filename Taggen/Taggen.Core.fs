[<AutoOpen>]
module Taggen.Core
open System
open System.Text.RegularExpressions
open System.Collections
open System.Web

let private RegMatch pattern input =
    let matches = Regex.Matches(input, pattern, RegexOptions.Compiled)
    matches |> Seq.cast |> Seq.toList
    |> List.map (fun (m : Match) ->
        [for g in m.Groups -> (g.Value)])

let inline private foldStrings strings =
    List.fold (fun acc item -> acc + item) "" strings

let private writeTagname tagText = 
    RegMatch (@"\w+") tagText
    |> List.head 
    |> List.head

let private writeClasses tagText =
    let classes = RegMatch (@"\.\w+") tagText |> List.concat |> List.map (fun dottedName -> dottedName.Trim '.')
    match List.length classes with
    | 0 -> ""
    | 1 -> List.head classes
    | _ -> List.fold (fun acc item -> acc + " " + item) (List.head classes) (List.tail classes)

let private writeId tagText =
    let matches = RegMatch (@"#\w+") tagText
    if (List.length matches > 1) then 
        failwith "An element can only have one id"
    else
        List.map (fun groups -> groups |> List.map (fun (x : string) -> x.Trim '#') |> foldStrings) matches |> foldStrings

let private writeAttr attr = 
    attr 
    |> List.map (fun (key, value) -> sprintf " %s=\"%s\"" key value)
    |> foldStrings

type Fragment =
    | Text of string
    | RawText of string
    | FragAttr of string * List<string * string> * List<Fragment> 

let unpackClasses tag =
    let classes = writeClasses tag
    let id = writeId tag
    [
        if not <| String.IsNullOrWhiteSpace classes then
            yield "class", classes
        if not <| String.IsNullOrWhiteSpace id then
            yield "id", id
    ]

let rec updateAttr frag newAttrs =
    match frag with
    | FragAttr (tagName, attr, children) ->
        match newAttrs with
        | [] -> frag
        | h::t -> 
            let replaceAttr currentAttr =
                if List.exists (fun old -> (fst old) = (fst h)) currentAttr then
                    List.map (fun old -> if (fst old) = (fst h) then h else old) currentAttr
                else
                    h::currentAttr

            let newFrag =
                FragAttr (tagName, replaceAttr attr, children)
            updateAttr newFrag t
    | _ -> failwith "You can't add attributes to a text fragment."

let Frag (tag, fragments) =
    let tagName = writeTagname tag
    FragAttr (tagName, unpackClasses tag, fragments)

let rec printFrag fragment =
    let strFrag frag =
        let tagName, attr, children = frag
        let attrs = writeAttr attr
        let openContents = sprintf "%s%s" tagName attrs
        let innerText = List.map printFrag children |> foldStrings
        if children = List.empty then
            sprintf "<%s/>" openContents
        else
            sprintf "<%s>%s</%s>" openContents innerText tagName

    match fragment with
    | Text t -> HttpUtility.HtmlEncode t
    | RawText t -> t
    | FragAttr (tag, attr, children) ->
        strFrag (tag, attr, children)