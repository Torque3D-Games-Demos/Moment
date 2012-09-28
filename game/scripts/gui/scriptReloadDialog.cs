//-----------------------------------------------------------------------------
// Copyright (c) 2012 Daniel Buckmaster
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

function ScriptReloadDlg::onWake(%this)
{
   %this.fileCount = 0;
   %f = 0;
   %fs = "*.cs";
   %file = findFirstFile(%fs);
   while(%file !$= "")
   {
      %this.fileCount++;
      %this.files[%f] = %file;
      %f++;
      %file = findNextFile(%fs);
   }
}

function ScriptReloadDlg::onSleep(%this)
{
}

function ScriptReloadDlgFileName::onKey(%this)
{
   %this.fileID = -1;
   %pos = %this.getCursorPos();
   %text = getSubstr(%this.getText(), 0, %pos);
   if(!strlen(%text))
   {
      %this.setText("");
      return;
   }
   %setText = "";
   %extraFileCount = 0;
   for(%f = 0; %f < ScriptReloadDlg.fileCount; %f++)
   {
      %file = fileName(ScriptReloadDlg.files[%f]);
      // If file name begins with text, auto-complete.
      if(strstr(%file, %text) == 0)
      {
         if(%setText $= "")
         {
            %setText = %file;
            %this.fileID = %f;
         }
         else
            %extraFileCount++;
      }
   }
   if(%setText !$= "")
   {
      if(%extraFileCount > 0)
         %setText = %setText SPC "(+" @ %extraFileCount @ ")";
      %this.setText(%setText);
      %this.setCursorPos(%pos);
   }
}

function ScriptReloadDlgFileName::onReturn(%this)
{
   if(%this.fileID != -1)
   {
      %file = ScriptReloadDlg.files[%this.fileID];
      exec(%file);
      Canvas.popDialog(ScriptReloadDlg);
   }
}
