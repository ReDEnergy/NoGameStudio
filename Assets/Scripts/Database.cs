﻿using UnityEngine;
using System.Collections;



public class LevelData
{
	public bool isUnlocked;
	public int	starsCount;
	public int	bestCombo;
}


public class Database
{
	static public bool isInitialized = false;

	static public int	currLvIndex = -1;
	static public Level currLevel;

	static public LevelData[] levels;

	static public bool gameON = true;

}