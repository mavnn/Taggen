# Taggen

## Light weight HTML generation in F#

A simple DSL for generating HTML and XML within F#. Still under heavy development for a personal project.

A strongly typed page template might look like this using the raw Core
library:

``` fsharp
open Taggen.Core

let frameWork title contentFrag =
    Frag("html",
        [
            Frag("head", [Frag("title", [Text title])])
            Frag("body",
                [
                    Frag("nav",
                        [
                            Frag("ul#navItems",
                                [
                                    Frag("li#homeLink.navItem",
                                        [
                                            FragAttr("a", Some [("href", "/")], [Text "Home"])
                                        ])
                                ])
                        ])
                    Frag("div#content", contentFrag)
                ])
        ])

```

(It's type is `string -> List<Fragment> -> Fragment`)

or using the various helpers available:

``` fsharp
open Taggen.Core
open Taggen.HtmlHelpers
open Taggen.Punctuation
open Taggen.Utils

let conciseFramework pageTitle contentFrag =
    html
    +<> (head +<> title % pageTitle)
    +<> (body
        +<> (nav
            +<> (ul %. "navItems"
                +<> (li %. "#homeLink.navItem" +<> linkTo "Home" "/")))
        +<> (div %. "#content"
            +<< contentFrag))
```

After construction, you can call them as standard F# functions:

``` fsharp
> printFrag (frameWork "My title" [Frag("p",[Text "An article here"])])

val it : string =
  "<html><head><title>My title</title></head><body><nav><ul id="navItems"><li id="homeLink" class="navItem"><a href="/">Home</a></li></ul></nav><div id="content"><p>An article here</p></div></body></html>"
```

or

``` fsharp
> !(conciseFramework "My title" [p_ "An article here"])

val it : string =
  "<html><head><title>My title</title></head><body><nav><ul><li id="homeLink" class="navItem"><a href="Home">/</a></li></ul></nav><div id="content"><p>An article here</p></div></body></html>"
```

Pretty printing is provided via the .net XmlDocument class (note this
will fail if your Taggen fragments aren't valid XML):

``` fsharp
> !!(conciseFramework "My title" [p_ "An article here"])

val it : string =
  "<html>
  <head>
    <title>My title</title>
  </head>
  <body>
    <nav>
      <ul>
        <li id="homeLink" class="navItem">
          <a href="Home">/</a>
        </li>
      </ul>
    </nav>
    <div id="content">
      <p>An article here</p>
    </div>
  </body>
</html>"
```

Check out scratchpad.fsx for examples of it's current state, including experiments with various helper methods and styles.

### Todo

* Write more performant foldStrings method
* Experiment with right and left associative infix 'punctuation' to ease fragment creation
* ? use Manos as default host/get routing etc?
* Add doctype helpers
