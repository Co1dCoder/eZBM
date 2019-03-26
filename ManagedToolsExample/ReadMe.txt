ManagedToolsExample demonstrates managed DgnPrimitiveTool and DgnElementSetTool for placing primitive elements and modifying elements respectively.

This example provides three tools, one for placing a Grouped Hole element and the other for modifying the Grouped Hole element. Modification is only restricted to fill color of the element. If the element has a fill color, the the tool will remove that fill, otherwise it will add a fill color to the element.

Below are the key-ins provided by this example

TOOLSEXAMPLE PLACE GROUPEDHOLE 
TOOLSEXAMPLE MODIFY GROUPEDHOLE

The last tool is edge blend tool. It implements dot net wrapper of LocateSubEntityTool.  It allows user
to select the edegs to be blended and expect the radius of blend and allow tangency option from user.
Once accepted it performs the blend operation
TOOLSEXAMPLE BLEND BLENDEDGE