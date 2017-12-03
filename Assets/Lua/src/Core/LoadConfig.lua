-- This is auto created by Editor
-- Author :Magus
-- Date : 11/30/2017 5:16:19 PM
--
print("LoadConfig")
local __configHash = {}
local __folder = "Config_CH/DataTable/"
local type, next ,error, pairs, ipairs, unpack , require, unrequire, setmetatable = type, next, error, pairs, ipairs, unpack, require, unrequire, setmetatable
return {localConfig=function(tname)
	local ret= __configHash[tname]
	if nil ~= ret then return ret end
	local __kidx, __keys, __data, __children,__chash, __hash, __seq = unpack(require(__folder..tname))
    local __count, __ccount = #__data, #__children
	 local __single, __double = #__kidx == 1, __ccount == 0
	local __cache, __ccache, __mcache = {}, {}, {}
	local __key1, __key2 = __keys[__kidx[1] + 1], (not __single) and __keys[__kidx[2] + 1]
	local function newindex(t,k,v) error("!!Readonly!!", 2) end
	local function vectorindex(t,k) if k == 'x' then return t[1] elseif k == 'y' then return t[2] elseif k == 'z' then return t[3] end end
	
	
	local function __internalindex(t, id)
		local raw = (t or __data)[id]
		if nil == raw then return nil end
        (t or __data)[id] = nil
		local ret = {}
		local si, ei = 1, #__keys 
		if t == __children then
            si = __kidx[2] + 1
		elseif not (__single or __double) then
            ei = __kidx[2]
		end
        for i = si, ei do
            local v = __keys[i]
            local tbl = raw[i - si + 1]
            if (type(tbl) == "table") then 
                for ii, vv in ipairs(tbl) do
                    if (type(vv) == "table") then setmetatable(vv,{__index=vectorindex}) end
                end
                setmetatable(tbl,{__index=vectorindex})
            end
            ret[v] = tbl
		end
        return ret
	end
	
	local function index(____, ln)
        local ret = __cache[ln]
        if nil ~= ret then return ret end
        ret = __internalindex(__data, ln)
        __cache[ln] = ret
		return ret
	end
	local function indexhash(____, k)
		k = __hash[k]
		if (nil == k) then return nil end
        return index(__data, k)
	end
	local function indexdoublehash(____, k)
        local cr = __mcache[k]
        if nil ~= cr then return cr end
		local kt = __hash[k]
		if nil == kt then return nil end
		local tbl = {}
		for k, v in pairs(kt) do
            tbl[k] = index(__data, v)
		end
		local seq = __seq[k]
		local num = #seq
        local pairsfunc = function(tt, k)
            local id = tt.id + 1
            if id > num then return end
            tt.id = id
            local rr = index(nil, seq[id])
            return rr[__key2], rr
		end
        cr = setmetatable({}, {__index=tbl,__newindex=newindex,__pairs=function() return pairsfunc, {id=0}, 0 end, __count = num})
        __mcache[k] = cr
        return cr
	end
    local function cindex(t, ln)
        local ret = __ccache[ln]
        if nil ~= ret then return ret end
        ret = __internalindex(t, ln)
        __ccache[ln] = setmetatable(ret, {__index=index(__data, __hash[ret.mainkey])})
        return ret
	end
    local function indexmotherhash(____, k)
        local cr = __mcache[k]
        if nil ~= cr then return cr end
        local kt = __chash[k]
        if nil == kt then return nil end
        local tbl = {}
        for k, v in pairs(kt) do
            tbl[k] = cindex(__children, v)
        end
        local seq = (__seq[k] or __chash[k])
        local num = #seq
        local pairsfunc = function(tt, k)
            local id = tt.id + 1
            if id > num then return end
            tt.id = id
            local rr = cindex(nil, seq[id])
            return rr[__key2], rr
        end
        cr = setmetatable({}, {__index=tbl,__newindex=newindex,__pairs=function() return pairsfunc, {id=0}, 0 end, __count = num})
        __mcache[k] = cr
        return cr
	end
	local function __iteratorfunc(t, id)
		id = id + 1
		if id > __count then return end
		return id, index(t, id)
	end
	local function __pairsfunc(tt, k)
        local id = tt.id + 1
        if id > __count then return end
        tt.id = id
        local ret = index(nil, id)
        return ret[__key1], ret
	end
	local function __dpairsfunc(tt, k)
        local id = tt.id + 1
        if id > __count then return end
        tt.id = id
        local mr = index(nil, id)
        k = mr[__key1]
        local seq = __seq[k]
        id = id + #seq - 1
        tt.id = id
        return k, indexdoublehash(nil,k)
	end
	local function __mpairsfunc(tt, k)
        local id = tt.id + 1
        if id > __count then return end
        tt.id = id
        local mr = index(nil, id)
        k = mr[__key1]
        return k, indexmotherhash(nil,k)
	end
    local function __miteratorfunc(t, id)
        id = id + 1
        if id > __ccount then return end
        return id, cindex(t, id)
	end
    local function __iterator() return __iteratorfunc, nil , 0 end
    local function __pairs() return __pairsfunc, {id=0} , nil end
    local function __dpairs() return __dpairsfunc, {id=0} , nil end
    local function __mpairs() return __mpairsfunc, {id=0}, nil end
    local function __miterator() return __miteratorfunc, nil, 0 end
    local function __at(i) return index(nil, i) end
    local function __mat(i) return cindex(nil, i) end
	if __hash == nil then
        ret = setmetatable({},{__index=index,__newindex=newindex,__iterator=__iterator,__pairs=__iterator,__count=__count,__at=__at})
	elseif __single then
        ret = setmetatable({},{__index=indexhash,__newindex=newindex,__iterator=__iterator,__pairs=__pairs,__count=__count,__at=__at})
	elseif __double then
        ret = setmetatable({},{__index=indexdoublehash,__newindex=newindex,__iterator=__iterator,__pairs=__dpairs,__count=__count,__at=__at})
    else
        ret = setmetatable({},{__index=indexmotherhash,__newindex=newindex,__iterator=__miterator,__pairs=__mpairs,__count=__count,__at=__mat})
	end
	__configHash[tname] = ret
	return ret
end,
UnloadConfig = function(tname)
    __configHash[tname] = nil
    unrequire(__folder..tname)
end,
UnloadAllConfigs = function()
    for k, v in pairs(__configHash) do
        unrequire(__folder..k)
    end
    __configHash = {}
end,
}