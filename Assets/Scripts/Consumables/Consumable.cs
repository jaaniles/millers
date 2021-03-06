﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable<T>
{
	bool Consume(T user);
}