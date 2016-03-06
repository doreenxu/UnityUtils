{\rtf1\ansi\ansicpg1252\cocoartf1404\cocoasubrtf340
{\fonttbl\f0\fmodern\fcharset0 CourierNewPS-BoldMT;\f1\fmodern\fcharset0 CourierNewPSMT;}
{\colortbl;\red255\green255\blue255;\red109\green109\blue109;\red14\green14\blue14;\red251\green0\blue129;
}
\paperw11900\paperh16840\margl1440\margr1440\vieww10800\viewh8400\viewkind0
\deftab720
\pard\pardeftab720\partightenfactor0

\f0\b\fs24 \cf2 \expnd0\expndtw0\kerning0
#!/bin/sh
\f1\b0 \cf3 \
\'a0\
THIS_DIR=`\cf4 dirname\cf3  $0`\
\pard\pardeftab720\partightenfactor0
\cf4 pushd\cf3  $THIS_DIR > /dev/null 2>&1\
\
mono $CSSCRIPT_DIR/cscs.exe -\cf4 nl\cf3  /Users/damarind/Unity/UnityUtils/Tiled2UnityLite-1.0.4.2/Tiled2UnityLite.cs $* &> /Users/damarind/Unity/UnityUtils/Tiled2UnityLite-1.0.4.2/tiled2unitylite.log\
\'a0\
\cf4 popd\cf3  > /dev/null 2<&1}