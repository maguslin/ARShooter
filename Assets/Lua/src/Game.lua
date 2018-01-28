require("src.GameInit");
Game = {}
function Game.start()
	print("Game.start()=====");
end
function Game.initLoadUI()
	print("==============load======================");
	--GameLoopManager:Start();
	--UIManager = require "src/Core/UIManager";
	--UIManager:init();
    --ResManager:init();
	MagusSceneManager.Instance:SetNextScene (SceneID.Game);
end
--Ïú»Ù--
function Game.OnDestroy()
	print('OnDestroy--->>>');
end