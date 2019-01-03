using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 마우스 버튼
public enum EMouseButton
{
	LEFT,
	RIGHT,
	MIDDLE,
	NONE
}

//! 게임 단계
public enum EGamePhase
{
	SELECT,
	MOVE,
	INFECT,
	TURNCHANGE,
	GAMEOVER,
	NONE
}

//! 세균 색깔
public enum EGermColor
{
	BLUE,
	RED,
	NONE
}

//! 전역 상수
public static partial class KDefine
{
    // 해상도
    public static readonly int SCREEN_WIDTH = 1280;
    public static readonly int SCREEN_HEIGHT = 720;

    public static readonly float UNIT_SCALE = 0.01f;
}