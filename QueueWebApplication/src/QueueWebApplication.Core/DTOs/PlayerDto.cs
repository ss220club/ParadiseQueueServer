﻿using System.Text;

namespace QueueWebApplication.Core.DTOs;

public record PlayerDto(
	string Ckey,
	string Role = "",
	int DonateTier = 0,
	bool Ban = false,
	params int[] WhitelistPasses);