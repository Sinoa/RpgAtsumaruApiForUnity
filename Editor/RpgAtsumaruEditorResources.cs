﻿// zlib/libpng License
//
// Copyright (c) 2018 Sinoa
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.

namespace RpgAtsumaruApiForUnity.Editor
{
    /// <summary>
    /// RPGアツマールAPI for Unityのエディタにて利用される、様々なバイナリデータを保持しているクラスです
    /// </summary>
    public static class RpgAtsumaruEditorResources
    {
        /// <summary>
        /// WebGLのテンプレートHTMLファイルの内容を保持しています
        /// </summary>
        public static readonly string WebGlHtmlData = "PCFET0NUWVBFIGh0bWw+DQo8aHRtbCBsYW5nPSJqYSI+DQogICAgPGhlYWQ+DQogICAgICAgIDxtZXRhIGNoYXJzZXQ9IlVURi04IiAvPg0KICAgICAgICA8bWV0YSBodHRwLWVxdWl2PSJDb250ZW50LVR5cGUiIGNvbnRlbnQ9InRleHQvaHRtbDsgY2hhcnNldD1VVEYtOCI+DQogICAgICAgIDx0aXRsZT4lVU5JVFlfV0VCX05BTUUlPC90aXRsZT4NCiAgICAgICAgPHN0eWxlIHR5cGU9InRleHQvY3NzIj4NCi5sb2dvDQp7DQogICAgcG9zaXRpb246IGFic29sdXRlOw0KICAgIGxlZnQ6IDBweDsNCiAgICB0b3A6IDBweDsNCiAgICB3aWR0aDogMTAwcHg7DQogICAgaGVpZ2h0OiAxMDBweDsNCiAgICBiYWNrZ3JvdW5kOiB1cmwoIiVBVFNVTUFSVV9MT0dPX0RBVEFVUkwlIik7DQp9DQoNCg0KLnN0YXR1c0xhYmVsDQp7DQogICAgcG9zaXRpb246IGFic29sdXRlOw0KICAgIGxlZnQ6IDEwcHg7DQogICAgdG9wOiAxMHB4Ow0KICAgIHdpZHRoOiBhdXRvOw0KICAgIGhlaWdodDogYXV0bzsNCiAgICBjb2xvcjogI0ZGRkZGRjsNCn0NCiAgICAgICAgPC9zdHlsZT4NCiAgICAgICAgPHNjcmlwdCBzcmM9IiVVTklUWV9XRUJHTF9MT0FERVJfVVJMJSI+PC9zY3JpcHQ+DQogICAgICAgIDxzY3JpcHQ+DQpmdW5jdGlvbiBPblByb2dyZXNzKGdhbWVJbnN0YW5jZSwgcHJvZ3Jlc3MpDQp7DQogICAgaWYgKCFnYW1lSW5zdGFuY2UuTW9kdWxlKQ0KICAgIHsNCiAgICAgICAgcmV0dXJuOw0KICAgIH0NCg0KDQogICAgaWYgKCFnYW1lSW5zdGFuY2UubG9nbykNCiAgICB7DQogICAgICAgIHZhciBsb2dvRWxlbWVudCA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoImRpdiIpOw0KICAgICAgICBsb2dvRWxlbWVudC5jbGFzc05hbWUgPSAibG9nbyI7DQogICAgICAgIGxvZ29FbGVtZW50LnN0eWxlLmxlZnQgPSAoKCVVTklUWV9XSURUSCUgLSAlQVRTVU1BUlVfTE9HT19XSURUSCUpIC8gMi4wKSArICJweCI7DQogICAgICAgIGxvZ29FbGVtZW50LnN0eWxlLnRvcCA9ICgoJVVOSVRZX0hFSUdIVCUgLSAlQVRTVU1BUlVfTE9HT19IRUlHSFQlKSAvIDIuMCkgKyAicHgiOw0KICAgICAgICBsb2dvRWxlbWVudC5zdHlsZS53aWR0aCA9ICIlQVRTVU1BUlVfTE9HT19XSURUSCVweCI7DQogICAgICAgIGxvZ29FbGVtZW50LnN0eWxlLmhlaWdodCA9ICIlQVRTVU1BUlVfTE9HT19IRUlHSFQlcHgiOw0KICAgICAgICBnYW1lSW5zdGFuY2UubG9nbyA9IGxvZ29FbGVtZW50Ow0KICAgICAgICBnYW1lSW5zdGFuY2UuY29udGFpbmVyLmFwcGVuZENoaWxkKGxvZ29FbGVtZW50KTsNCiAgICB9DQoNCg0KICAgIGlmICghZ2FtZUluc3RhbmNlLnN0YXR1c0xhYmVsKQ0KICAgIHsNCiAgICAgICAgdmFyIHN0YXR1c0xhYmVsID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgiZGl2Iik7DQogICAgICAgIHN0YXR1c0xhYmVsLmNsYXNzTmFtZSA9ICJzdGF0dXNMYWJlbCI7DQogICAgICAgIGdhbWVJbnN0YW5jZS5zdGF0dXNMYWJlbCA9IHN0YXR1c0xhYmVsOw0KICAgICAgICBnYW1lSW5zdGFuY2UuY29udGFpbmVyLmFwcGVuZENoaWxkKHN0YXR1c0xhYmVsKTsNCiAgICB9DQoNCg0KICAgIHZhciBwcm9jZXNzZWRQcm9ncmVzcyA9IE1hdGguZmxvb3IocHJvZ3Jlc3MgKiAxMDAwMC4wKSAvIDEwMC4wOw0KICAgIGdhbWVJbnN0YW5jZS5zdGF0dXNMYWJlbC5pbm5lclRleHQgPSAi44Ky44O844Og44OH44O844K/44KS6Kqt44G/6L6844G/5LitICIgKyBwcm9jZXNzZWRQcm9ncmVzcyArICIlIjsNCg0KDQogICAgaWYgKHByb2dyZXNzID09IDEpDQogICAgew0KICAgICAgICBnYW1lSW5zdGFuY2UubG9nby5zdHlsZS5kaXNwbGF5ID0gIm5vbmUiOw0KICAgIH0NCn0NCg0KDQpVbml0eUxvYWRlci5pbnN0YW50aWF0ZSgiZ2FtZUNvbnRhaW5lciIsICIlVU5JVFlfV0VCR0xfQlVJTERfVVJMJSIsIHtvblByb2dyZXNzOiBPblByb2dyZXNzfSk7DQogICAgICAgIDwvc2NyaXB0Pg0KICAgIDwvaGVhZD4NCiAgICA8Ym9keT4NCiAgICAgICAgPGRpdiBpZD0iZ2FtZUNvbnRhaW5lciIgc3R5bGU9IndpZHRoOiAlVU5JVFlfV0lEVEglcHg7IGhlaWdodDogJVVOSVRZX0hFSUdIVCVweDsgbWFyZ2luOiBhdXRvIj48L2Rpdj4NCiAgICA8L2JvZHk+DQo8L2h0bWw+";


        /// <summary>
        /// 1x1の透明png画像データです
        /// </summary>
        public static readonly string TransparentDotImage = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAACXBIWXMAADXUAAA11AFeZeUIAAAADUlEQVQImWNgYGBgAAAABQABh6FO1AAAAABJRU5ErkJggg==";


        /// <summary>
        /// 1x1の透明png画像データをDataUrl形式として表現したデータです
        /// </summary>
        public static readonly string TransparentDotImageDataUrl = "data:image/png;base64," + TransparentDotImage;
    }
}