//-----------------------------------------------------------------------------
// Copyright (c) 2012 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

//------------------------------------------------------------------------------
// Debugging Functions
//------------------------------------------------------------------------------

$MFDebugRenderMode = 0;
function cycleDebugRenderMode(%val)
{
   if (!%val)
      return;

   $MFDebugRenderMode++;

   if ($MFDebugRenderMode > 16)
      $MFDebugRenderMode = 0;
   if ($MFDebugRenderMode == 15)
      $MFDebugRenderMode = 16;

   setInteriorRenderMode($MFDebugRenderMode);

   if (isObject(ChatHud))
   {
      %message = "Setting Interior debug render mode to ";
      %debugMode = "Unknown";

      switch($MFDebugRenderMode)
      {
         case 0:
            %debugMode = "NormalRender";
         case 1:
            %debugMode = "NormalRenderLines";
         case 2:
            %debugMode = "ShowDetail";
         case 3:
            %debugMode = "ShowAmbiguous";
         case 4:
            %debugMode = "ShowOrphan";
         case 5:
            %debugMode = "ShowLightmaps";
         case 6:
            %debugMode = "ShowTexturesOnly";
         case 7:
            %debugMode = "ShowPortalZones";
         case 8:
            %debugMode = "ShowOutsideVisible";
         case 9:
            %debugMode = "ShowCollisionFans";
         case 10:
            %debugMode = "ShowStrips";
         case 11:
            %debugMode = "ShowNullSurfaces";
         case 12:
            %debugMode = "ShowLargeTextures";
         case 13:
            %debugMode = "ShowHullSurfaces";
         case 14:
            %debugMode = "ShowVehicleHullSurfaces";
         // Depreciated
         //case 15:
         //   %debugMode = "ShowVertexColors";
         case 16:
            %debugMode = "ShowDetailLevel";
      }

      ChatHud.addLine(%message @ %debugMode);
   }
}

GlobalActionMap.bind(keyboard, "F9", cycleDebugRenderMode);

//------------------------------------------------------------------------------
//
// Start profiler by pressing ctrl f3
// ctrl f3 - starts profile that will dump to console and file
//
function doProfile(%val)
{
   if (%val)
   {
      // key down -- start profile
      echo("Starting profile session...");
      profilerReset();
      profilerEnable(true);
   }
   else
   {
      // key up -- finish off profile
      echo("Ending profile session...");

      profilerDumpToFile("profilerDumpToFile" @ getSimTime() @ ".txt");
      profilerEnable(false);
   }
}

GlobalActionMap.bind(keyboard, "ctrl F3", doProfile);

//------------------------------------------------------------------------------
// Misc.
//------------------------------------------------------------------------------

GlobalActionMap.bind(keyboard, "tilde", toggleConsole);
GlobalActionMap.bindCmd(keyboard, "alt k", "cls();","");
